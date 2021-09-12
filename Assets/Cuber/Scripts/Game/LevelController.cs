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

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            bool nextState = !PauseMenu.Instance.PauseMenuActive;
            PauseMenu.Instance.ShowPauseMenu(nextState);

            if(!nextState)
                PauseMenu.Instance.ShowLevelSelector(nextState);
        }
    }

    private void OnFloorButtonPressed(Message message)
    {
        FloorButtonPressedMessage castmsg = message as FloorButtonPressedMessage;

        if(castmsg.button == _exitButton)
        {
            if(castmsg.isPressed)
            {
                SceneLoader.Instance.LoadNextLevel();
                BlockOnLevelFinish(true);
            }
        }
    }

    private IEnumerator StartSequence()
    {
        FadeController.Instance.Fade(false, 1f);

        if (CameraController.Instace != null)
            CameraController.Instace.EnableCamera(true);
        else
            Debug.LogError("No cam controller");

        yield return new WaitForSeconds(1f);

        BlockOnLevelFinish(false);
    }

    private void BlockOnLevelFinish(bool block)
    {
        CubeController.Instance.AddOrRemoveBlocker("LevelIsFinished", gameObject, block);
    }
    #endregion

    #region Gizmos
    #endregion

    #region Classes
    #endregion
}
