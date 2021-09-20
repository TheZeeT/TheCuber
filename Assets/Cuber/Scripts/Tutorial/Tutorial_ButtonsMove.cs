using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial_ButtonsMove : MonoBehaviour
{
    #region Inspector
    [SerializeField] private Transform _follow;
    #endregion

    #region Private
    #endregion

    #region Public
    #endregion

    #region Functions
    private void Awake()
    {

    }

    private void LateUpdate()
    {
        transform.position = new Vector3(_follow.position.x, Mathf.Round(_follow.position.y), _follow.position.z);
    }
    #endregion

    #region Gizmos
    #endregion

    #region Classes
    #endregion
}
