using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicCubeRotator : MonoBehaviour
{
    #region Inspector
    [SerializeField] private Transform _cube;
    [SerializeField] private Transform _camera;
    [SerializeField] private float _speed = 0.1f;
    [Header("Objects")]
    [SerializeField] private GameObject _objPX;
    [SerializeField] private GameObject _objNX;
    [SerializeField] private GameObject _objPY;
    [SerializeField] private GameObject _objNY;
    [SerializeField] private GameObject _objPZ;
    [SerializeField] private GameObject _objNZ;
    [Header("debug")]
    [SerializeField] private bool _autoRotate;
    #endregion

    #region Private
    private Vector3 _rotationTarget;
    private Vector3 _rotationCurrent;
    #endregion

    #region Public
    #endregion

    #region Functions
    private void Awake()
    {
        _rotationTarget = Vector3.one;
        _rotationCurrent = Vector3.one;
    }

    private void Update()
    {
        //Debug.Log($"Random Vector DOT = {Vector3.Dot(_rotationCurrent.normalized, _rotationTarget.normalized)}");
        if (Vector3.Dot(_rotationCurrent.normalized, _rotationTarget.normalized) > 0.9f)
        {
            //Vector3 random = Random.insideUnitSphere;
            _rotationTarget = Random.insideUnitSphere;
            // new Vector3(Mathf.Abs(random.x), Mathf.Abs(random.y), Mathf.Abs(random.z));
            //Debug.Log($"Random Vector chage to {_rotationTarget}");
        }

        _rotationCurrent = Vector3.MoveTowards(_rotationCurrent, _rotationTarget, _speed);

        if (_autoRotate)
            _cube.Rotate(_rotationCurrent);

        Vector3 _camDir = _camera.transform.forward * -1; 

        //Get direction for every pair of objects
        float x = Vector3.Dot(_cube.right, _camDir);
        float y = Vector3.Dot(_cube.up, _camDir);
        float z = Vector3.Dot(_cube.forward, _camDir);

        _objPX.SetActive(x > 0);
        _objNX.SetActive(x < 0);
        _objPY.SetActive(y > 0);
        _objNY.SetActive(y < 0);
        _objPZ.SetActive(z > 0);
        _objNZ.SetActive(z < 0);
    }
    #endregion

    #region Gizmos
    #endregion

    #region Classes
    #endregion
}
