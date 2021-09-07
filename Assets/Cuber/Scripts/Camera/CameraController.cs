using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace TheCuber.Cube
{
    public class CameraController : MonoBehaviour
    {
        #region Inspector
        [SerializeField] private Transform _cameraTransform;
        [SerializeField] private Camera _cam;
        [SerializeField] private Color[] _bgColors;
        [SerializeField] private float _changeSpeed = 0.001f;
        #endregion

        #region Private
        private float _colorIdValue;
        #endregion

        #region Public
        public static CameraController Instace { get; private set; }
        #endregion

        #region Functions
        private void Awake()
        {
            Instace = this;
            GameObject.DontDestroyOnLoad(this.gameObject);
            EnableCamera(false);
        }

        private void LateUpdate()
        {
            if (CubeController.Instance.CurrentCube != null)
            {
                transform.position = CubeController.Instance.CurrentCube.transform.position;
            }

            if (_cam.gameObject.activeInHierarchy)
                _cam.backgroundColor = GetLerpColor();

            //CheckCameraRotation();
        }

        private Color GetLerpColor()
        {
            _colorIdValue += _changeSpeed;

            return Color.Lerp(
                _bgColors[Mathf.FloorToInt(_colorIdValue) % (_bgColors.Length - 1)],
                _bgColors[(Mathf.FloorToInt(_colorIdValue) % (_bgColors.Length - 1)) + 1],
                _colorIdValue % 1f);
        }

        public void EnableCamera(bool enable)
        {
            _cameraTransform.gameObject.SetActive(enable);
        }
        #endregion

        #region Gizmos
        #endregion

        #region Classes
        #endregion
    }
}
