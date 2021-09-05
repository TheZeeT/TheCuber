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
        #endregion

        #region Public
        public static CubeController Instance
        {
            get { return _instance; }
        }

        public bool IsMoving { get;  set; }
        //public bool IsCameraRotating { get;  set; }
        //public Vector3 UpDirection { get;  set; }
        //public Vector3 ForwardDirection { get; set; }
        public CubeMover CurrentCube { get; set; }

        public Vector3Int CurrentCubePosition { get; set; }
        #endregion

        #region Functions
        private void Awake()
        {
            _instance = this;
            //UpDirection = Vector3.up;
            //ForwardDirection = Vector3.forward;
        }
        #endregion

        #region Gizmos
        #endregion

        #region Classes
        #endregion
    }
}