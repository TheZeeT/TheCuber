using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TheCuber.Cube
{
    public class CubeController : MonoBehaviour
    {
        #region Inspector
        #endregion

        #region Private
        private static CubeController _instance;
        private Dictionary<string, GameObject> _blockers = new Dictionary<string, GameObject>();
        #endregion

        #region Public
        public static CubeController Instance
        {
            get { return _instance; }
        }

        public bool CanMove { get; private set; }
        public CubeMover CurrentCube { get; set; }

        public Vector3Int CurrentCubePosition { get; set; }
        #endregion

        #region Functions
        private void Awake()
        {
            _instance = this;
            GameObject.DontDestroyOnLoad(this.gameObject);
            //UpDirection = Vector3.up;
            //ForwardDirection = Vector3.forward;
        }

        private void Update()
        {
            if (!CubeController.Instance.CanMove) // || CubeController.Instance.IsCameraRotating)
                return;

            if (CurrentCube != null)
                CurrentCube.MoveCube(GetMoveDirection());
        }

        public void AddOrRemoveBlocker(string blockKey, GameObject obj, bool doAdd)
        {
            if (doAdd)
            {
                if (!_blockers.ContainsKey(blockKey))
                    _blockers.Add(blockKey, obj);
            }
            else
            {
                if (_blockers.ContainsKey(blockKey))
                    _blockers.Remove(blockKey);
            }

            CanMove = _blockers.Count == 0;
        }

        private Vector3 GetMoveDirection()
        {
            return new Vector3(
                Mathf.Round(Input.GetAxis("Horizontal")),
                0f,
                Mathf.Round(Input.GetAxis("Vertical")))
                .normalized;
        }
        #endregion

        #region Gizmos
        #endregion

        #region Classes
        #endregion
    }
}