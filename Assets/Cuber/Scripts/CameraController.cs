using DG.Tweening;
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
        private float _angle = 0f;
        #endregion

        #region Public
        #endregion

        #region Functions
        private void Start()
        {

        }

        private void LateUpdate()
        {
            if (_followTransform != null)
            {
                transform.position = _followTransform.position;
            }

            CheckCameraRotation();
        }

        private void CheckCameraRotation()
        {
            if (CubeController.Instance.IsCameraRotating || CubeController.Instance.IsMoving)
                return;

            if (Input.GetKeyDown(KeyCode.Q))
                RotateCamera(1);
            else if (Input.GetKeyDown(KeyCode.E))
                RotateCamera(-1);


        }

        private void RotateCamera(int direction)
        {
            CubeController.Instance.IsCameraRotating = true;

            if (direction != 0)
            {
                _angle = (_angle + direction * 90f) % 360;
                _cameraTransform.DOLocalRotate(Vector3.up * _angle, 1f).OnComplete(() => EndCameraRotation());
            }
        }

        private void EndCameraRotation()
        {
            CubeController.Instance.IsCameraRotating = false;
            CubeController.Instance.ForwardDirection =
                new Vector3(
                    Mathf.Round(_cameraTransform.forward.normalized.x),
                    Mathf.Round(_cameraTransform.forward.normalized.y),
                    Mathf.Round(_cameraTransform.forward.normalized.z));

            CubeController.Instance.ResetStep();

            //Debug.Log($"CamTr FRWD = {_cameraTransform.forward} // CUBE = {CubeController.Instance.ForwardDirection}");
        }
        #endregion

        #region Gizmos
        #endregion

        #region Classes
        private void OnDrawGizmos()
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawRay(_cameraTransform.position, _cameraTransform.forward.normalized);
        }
        #endregion
    }
}
