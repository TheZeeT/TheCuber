using System;
using System.Collections;
using System.Collections.Generic;
using TheCuber.Cube;
using UnityEngine;

public class LevelController : MonoBehaviour
{
    #region Inspector
    [SerializeField] private FloorButton _exitButton;
    #endregion

    #region Private
    private Vector3Int _endPos;
    #endregion

    #region Public
    #endregion

    #region Functions
    private void Awake()
    {
        MessagingSystem.Instance.AttachListener(typeof(FloorButtonPressedMessage), OnFloorButtonPressed);
        StartCoroutine(StartSequence());
    }

    private void OnDestroy()
    {
        MessagingSystem.Instance.DetachListener(typeof(FloorButtonPressedMessage), OnFloorButtonPressed);
    }

    private void OnFloorButtonPressed(Message message)
    {
        FloorButtonPressedMessage castmsg = message as FloorButtonPressedMessage;

        if(castmsg.button == _exitButton)
        {
            if(castmsg.isPressed)
            {
                SceneLoader.Instance.LoadNextLevel();
            }
        }
    }

    private IEnumerator StartSequence()
    {
        FadeController.Instance.Fade(false, 2f);

        if (CameraController.Instace != null)
            CameraController.Instace.EnableCamera(true);
        else
            Debug.LogError("No cam controller");

        yield return new WaitForSeconds(1f);

        CubeController.Instance.IsMoving = false; // change to clear all blockers
    }
    #endregion

    #region Gizmos
    #endregion

    #region Classes
    #endregion
}
