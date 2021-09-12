using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;

namespace TheCuber.Cube
{
    public class CubeMover : MonoBehaviour
    {
        #region Inspector
        [SerializeField] private float _rollSpeed = 5;
        //[SerializeField] private Transform _follower;
        //[SerializeField] private Vector3 _newUp;
        //[SerializeField] private Vector3 _newFwrd;
        [SerializeField] private StatusEffects _effects;
        [SerializeField] private AudioSource _audio;
        #endregion

        #region Private
        private Vector3 lastPos;
        private Vector3 lastDir;
        private int _cubeLayerMask;
        private Coroutine _rollCoroutine = null;

        [Flags]
        public enum StatusEffects
        {
            Sticky = 1,
            Fast = 2
        }
        #endregion

        #region Public
        public Vector3 MoveDirection { get; private set; }
        #endregion

        #region Functions
        private void Start()
        {
            _cubeLayerMask = ~LayerMask.GetMask("Cube");
            CubeController.Instance.CurrentCube = this; 
            BlockMoving(false);
            //ResetStep();
        }

        private void OnDestroy()
        {
            CubeController.Instance.CurrentCube = null;
        }

        private void Update()
        {
            if (!CubeController.Instance.CanMove) // || CubeController.Instance.IsCameraRotating)
                return;

            GetMoveDirection();

            if (Math.Abs(Mathf.Round(MoveDirection.x) + Mathf.Round(MoveDirection.z)) == 1)
            {
                //Debug.Log($"Move Dir sum = {Mathf.Round(MoveDirection.x)} + {Mathf.Round(MoveDirection.z)}");
                AssembleVectors(MoveDirection);
            }
        }

        private void AssembleVectors(Vector3 dir)
        {
            Vector3Int roundPos = Vector3Int.FloorToInt(transform.position);
            Vector3Int roundDir = Vector3Int.RoundToInt(dir);

            bool hasEmptySpaceBelow = !Physics.Raycast(roundPos + roundDir, Vector3.down, 1f, _cubeLayerMask);
            bool hasEmptySpaceOnSameLevel = !Physics.Raycast(roundPos, roundDir, 1f, _cubeLayerMask);
            bool hasEmptySpaceAbove = !Physics.Raycast(roundPos + Vector3.up, roundDir, 1f, _cubeLayerMask);

            Vector3 anchor = transform.position + ((hasEmptySpaceOnSameLevel ? Vector3.down : Vector3.up) + roundDir) * 0.5f;
            Vector3 axis = Vector3.Cross(Vector3.up, dir);

            lastPos = anchor;
            lastDir = axis;

            int rollCount = (hasEmptySpaceOnSameLevel ? 1 : 2) + (hasEmptySpaceAbove ? 0 : -1);

            //Debug.Log($"rollCount = {rollCount} = {(hasEmptySpaceOnSameLevel ? 1 : 2)} + {(hasEmptySpaceAbove ? 0 : -1)}");

            if (rollCount > 0) // && (rollCount == 1 && hasEmptySpaceOnSameLevel ? !hasEmptySpaceBelow : true))
            {
                BlockMoving(true);

                MessagingSystem.Instance.TriggerMessage(new CubeMoveStartMessage(
                    this,
                    roundPos,
                    roundPos + roundDir));

                if (_rollCoroutine == null)
                    _rollCoroutine = StartCoroutine(Roll(anchor, axis, rollCount));
            }

        }
        private void GetMoveDirection()
        {
            if (CubeController.Instance.CurrentCube == this)
                MoveDirection = new Vector3(
                    Mathf.Round(Input.GetAxis("Horizontal")),
                    0f,
                    Mathf.Round(Input.GetAxis("Vertical")))
                    .normalized;
        }

        //public void ResetStep()
        //{
        //    _follower.position = transform.position;
        //    _follower.rotation = Quaternion.FromToRotation(Vector3.up, CubeController.Instance.UpDirection);
        //    _follower.rotation = Quaternion.LookRotation(CubeController.Instance.ForwardDirection);
        //}

        private IEnumerator Roll(Vector3 anchor, Vector3 axis, int rotations)
        {
            float stepsCount = (_effects.HasFlag(StatusEffects.Fast) ? 2 : 1) * _rollSpeed;

            //Debug.Log($"Roll steps = {90 / stepsCount * rotations}");

            for (var i = 0; i < 90 / stepsCount * rotations; i++)
            {
                transform.RotateAround(anchor, axis, stepsCount);
                yield return null;
            }

            if (!CheckFalling())
                EndRoll();

            _rollCoroutine = null;
        }

        private void EndRoll()
        {
            Vector3Int newPos = Vector3Int.RoundToInt(transform.position);

            _audio.Play();

            transform.position = newPos;
            //ResetStep();

            MessagingSystem.Instance.TriggerMessage(new CubeMoveEndMessage(this, CubeController.Instance.CurrentCubePosition, newPos));

            CubeController.Instance.CurrentCubePosition = newPos;

            BlockMoving(false);
        }

        private bool CheckFalling()
        {
            if (Physics.Raycast(transform.position, Vector3.down, 1f, _cubeLayerMask))
            {
                return false;
            }
            else
            {
                //Debug.Log($"No floor on end roll, now at {transform.position.y}");

                if (_effects.HasFlag(StatusEffects.Sticky))
                {
                    bool isHanging = (
                            Physics.Raycast(transform.position, Vector3.forward, 1f, _cubeLayerMask) ||
                            Physics.Raycast(transform.position, Vector3.right, 1f, _cubeLayerMask) ||
                            Physics.Raycast(transform.position, Vector3.back, 1f, _cubeLayerMask) ||
                            Physics.Raycast(transform.position, Vector3.left, 1f, _cubeLayerMask));

                    if (!isHanging)
                        FallDown();

                    return !isHanging;
                }
                else
                {
                    FallDown();

                    return true;
                }
            }
        }

        private void FallDown()
        {
            if (!Physics.Raycast(transform.position, Vector3.down, out RaycastHit hit, 100f, _cubeLayerMask))
            {
                Debug.LogError("Fall of the world");
                hit.point = new Vector3(transform.position.x, -100f, transform.position.z);
                CubeController.Instance.CurrentCube = null;
                SceneLoader.Instance.ReloadLevel();
            }

            float fallDuration = Mathf.Abs(transform.position.y - Mathf.Ceil(hit.point.y));

            transform.DOMoveY(Mathf.Ceil(hit.point.y), fallDuration / 10f)
                .SetEase(Ease.Linear)
                .OnComplete(() => EndRoll());
        }

        private void BlockMoving(bool block)
        {
            CubeController.Instance.AddOrRemoveBlocker("Moving", gameObject, block);
        }
        #endregion

        #region Gizmos
        private void OnDrawGizmos()
        {
            Gizmos.color = Color.magenta;
            Gizmos.DrawRay(transform.position + Vector3.up, MoveDirection * 2);

            //Gizmos.color = Color.green;
            //Gizmos.DrawRay(transform.position + Vector3.up, _newUp);
            //Gizmos.color = Color.blue;
            //Gizmos.DrawRay(transform.position + Vector3.up, _newFwrd);

            Gizmos.color = Color.yellow;
            Gizmos.DrawRay(lastPos, lastDir);

        }
        #endregion

        #region Classes
        #endregion
    }
}
