using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TheCuber.Cube
{
    public class CubeController : MonoBehaviour
    {
        #region Inspector
        [SerializeField] private CubeMover _mover;
        #endregion

        #region Private
        private static CubeController _instance;
        #endregion

        #region Public
        public static CubeController Instance
        {
            get { return _instance; }
        }

        public bool IsMoving { get;  set; }
        public bool IsCameraRotating { get;  set; }
        public Vector3 UpDirection { get;  set; }
        public Vector3 ForwardDirection { get; set; }
        #endregion

        #region Functions
        private void Awake()
        {
            _instance = this;
            UpDirection = Vector3.up;
            ForwardDirection = Vector3.forward;
        }

        public void ResetStep()
        {
            _mover.ResetStep();
        }

        //private void Update()
        //{
        //    if (Input.GetKey(KeyCode.F))
        //        Debug.Log($"Frw = {ForwardDirection}");
        //}
        #endregion

        #region Gizmos
        #endregion

        #region Classes
        #endregion
    }
}