using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(FloorButton))]
public class ButtonPanel : MonoBehaviour
{
    #region Inspector
    [SerializeField] private Renderer _renderer;
    #endregion

    #region Private
    #endregion

    #region Public
    #endregion

    #region Functions
    private void Awake()
    {
        MessagingSystem.Instance.AttachListener(typeof(FloorButtonPressedMessage), OnFloorButtonPressed);
        SetState(false);
    }

    private void OnDestroy()
    {
        MessagingSystem.Instance.AttachListener(typeof(FloorButtonPressedMessage), OnFloorButtonPressed);
    }

    private void OnFloorButtonPressed(Message message)
    {
        FloorButtonPressedMessage castmsg = message as FloorButtonPressedMessage;

        if(castmsg.button.gameObject == gameObject)
        {
            SetState(castmsg.isPressed);
        }
    }

    private void SetState(bool isActive)
    {
        _renderer.material.color = isActive ? Color.green : Color.black;
    }
    #endregion

    #region Gizmos
    #endregion

    #region Classes
    #endregion
}