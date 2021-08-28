using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoRotator : MonoBehaviour
{
    #region Inspector
    [SerializeField] private Vector3 _axis;
    #endregion

    #region Private
    #endregion

    #region Public
    #endregion

    #region Functions
    private void Update()
    {
        transform.Rotate(_axis);
    }
    #endregion

    #region Gizmos
    #endregion

    #region Classes
    #endregion
}
