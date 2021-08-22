using System.Collections;
using System.Collections.Generic;
using TheCuber.Cube;
using UnityEngine;

public class FloorFollower : MonoBehaviour
{
    #region Inspector
    #endregion

    #region Private
    private Renderer _renderer;
    #endregion

    #region Public
    #endregion

    #region Functions
    private void Start()
    {
        if (!TryGetComponent(out _renderer))
            Debug.LogError($"No Renderer Found on {name}", this);
    }

    private void Update()
    {
        _renderer.material.SetVector("_Position", CubeController.Instance.transform.position);
    }

    #endregion

    #region Gizmos
    #endregion

    #region Classes
    #endregion
}
