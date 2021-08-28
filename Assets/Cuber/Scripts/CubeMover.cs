using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TheCuber.Cube
{
    public class CubeMover : MonoBehaviour
    {
        #region Inspector
        [SerializeField] private float _rollSpeed = 5;
        [SerializeField] private Transform _follower;
        [SerializeField] private Vector3 _newUp;
        [SerializeField] private Vector3 _newFwrd;
        [SerializeField] private StatusEffects _effects;
        #endregion

        #region Private
        private Vector3 lastPos;
        private Vector3 lastDir;
        private int _cubeLayerMask;

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
            ResetStep();
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.F))
            {
                CubeController.Instance.UpDirection = _newUp;
                CubeController.Instance.ForwardDirection = _newFwrd;
                ResetStep();
                Debug.Log("Set new Vectors");
            }

            if (CubeController.Instance.IsMoving || CubeController.Instance.IsCameraRotating)
                return;

            GetMoveDirection();

            if (Math.Abs(Mathf.Round(MoveDirection.x) + Mathf.Round(MoveDirection.y) + Mathf.Round(MoveDirection.z)) == 1)
            {
                AssembleVectors(MoveDirection);
            }
        }

        private void AssembleVectors(Vector3 dir)
        {
            bool hasEmptySpaceBelow = !Physics.Raycast(transform.position + dir, Vector3.down, 1f, _cubeLayerMask);
            bool hasEmptySpaceOnSameLevel = !Physics.Raycast(transform.position, dir, 1f, _cubeLayerMask);
            bool hasEmptySpaceAbove = !Physics.Raycast(transform.position + Vector3.up, dir, 1f, _cubeLayerMask);

            Vector3 anchor = transform.position + ((hasEmptySpaceOnSameLevel ? Vector3.down : Vector3.up) + dir) * 0.5f;
            Vector3 axis = Vector3.Cross(Vector3.up, dir);

            lastPos = anchor;
            lastDir = axis;

            int rollCount = (hasEmptySpaceOnSameLevel ? 1 : 2) + (hasEmptySpaceAbove ? 0 : -1);

            if (rollCount > 0) // && (rollCount == 1 && hasEmptySpaceOnSameLevel ? !hasEmptySpaceBelow : true))
                StartCoroutine(Roll(anchor, axis, rollCount));


        }
        private void GetMoveDirection()
        {
            MoveDirection = _follower.TransformDirection(
                new Vector3(Input.GetAxis("Horizontal"), 0f, Input.GetAxis("Vertical"))).normalized;
        }

        public void ResetStep()
        {
            _follower.position = transform.position;
            _follower.rotation = Quaternion.FromToRotation(Vector3.up, CubeController.Instance.UpDirection);
            _follower.rotation = Quaternion.LookRotation(CubeController.Instance.ForwardDirection);
        }

        private IEnumerator Roll(Vector3 anchor, Vector3 axis, int rotations)
        {
            CubeController.Instance.IsMoving = true;

            for (var i = 0; i < 90 / _rollSpeed * rotations; i++)
            {
                transform.RotateAround(anchor, axis, _rollSpeed);
                yield return null;
            }

            if (!CheckFalling())
                EndRoll();
        }

        private void EndRoll()
        {
            transform.position = new Vector3(
                Mathf.Round(transform.position.x),
                Mathf.Round(transform.position.y),
                Mathf.Round(transform.position.z));

            ResetStep();

            CubeController.Instance.IsMoving = false;
        }

        private bool CheckFalling()
        {
            if (Physics.Raycast(transform.position, Vector3.down, 1f, _cubeLayerMask))
            {
                return false;
            }
            else
            {
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
            }

            float fallDuration = Mathf.Abs(transform.position.y - Mathf.Ceil(hit.point.y));

            transform.DOMoveY(Mathf.Ceil(hit.point.y), fallDuration / 10f)
                .SetEase(Ease.Linear)
                .OnComplete(() => EndRoll());
        }
        #endregion

        #region Gizmos
        private void OnDrawGizmos()
        {
            Gizmos.color = Color.magenta;
            Gizmos.DrawRay(transform.position + Vector3.up, MoveDirection * 2);

            Gizmos.color = Color.green;
            Gizmos.DrawRay(transform.position + Vector3.up, _newUp);
            Gizmos.color = Color.blue;
            Gizmos.DrawRay(transform.position + Vector3.up, _newFwrd);

            Gizmos.color = Color.yellow;
            Gizmos.DrawRay(lastPos, lastDir);

        }
        #endregion

        #region Classes
        #endregion
    }
}
