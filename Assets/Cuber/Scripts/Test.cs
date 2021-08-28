using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    #region Inspector
    [SerializeField] private bool _do;
    [SerializeField] private Vector3 _dir;
    #endregion

    #region Private
    #endregion

    #region Public
    #endregion

    #region Functions
    private void Awake()
    {

    }

    private void Update()
    {
        if (_do)
        {
            transform.rotation = Quaternion.FromToRotation(Vector3.up, _dir);
            _do = false;
        }

    }
    #endregion

    #region Gizmos
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawRay(transform.position, transform.up);
        Gizmos.color = Color.red;
        Gizmos.DrawRay(transform.position, _dir);
    }
    #endregion

    #region Classes
    #endregion
}
