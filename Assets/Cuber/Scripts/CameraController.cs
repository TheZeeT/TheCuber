using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TheCuber.Cube
{
    public class CameraController : MonoBehaviour
    {
        #region Inspector
        [SerializeField] private Vector3 _offsetPos;
        [SerializeField] private Vector3 _offsetRot;
        [SerializeField] private Transform _followTransform;
        [SerializeField] private Transform _cameraTransform;
        #endregion

        #region Private
        #endregion

        #region Public
        #endregion

        #region Functions
        private void Start()
        {

        }

        private void LateUpdate()
        {
            if(_followTransform != null)
            {
                transform.position = _followTransform.position;
            }    
        }

        #endregion

        #region Gizmos
        #endregion

        #region Classes
        #endregion
    }
}
