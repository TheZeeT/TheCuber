using System.Collections;
using System.Collections.Generic;
using TheCuber.Cube;
using UnityEngine;

public class LevelController : MonoBehaviour
{
    #region Inspector
    [SerializeField] private Transform _endBlock;
    #endregion

    #region Private
    private Vector3Int _endPos;
    #endregion

    #region Public
    #endregion

    #region Functions
    private void Awake()
    {
        _endPos = new Vector3Int(
            Mathf.RoundToInt(_endBlock.transform.position.x),
            Mathf.RoundToInt(_endBlock.transform.position.y),
            Mathf.RoundToInt(_endBlock.transform.position.z)
            );
    }

    private void LateUpdate()
    {
        if (CubeController.Instance.CurrentCube != null)
        {
            if (CubeController.Instance.CurrentCubePosition == _endPos)
            {
                Debug.Log($"Reached End");
            }
        }
    }
    #endregion

    #region Gizmos
    #endregion

    #region Classes
    #endregion
}
