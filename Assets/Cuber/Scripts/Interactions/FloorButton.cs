using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Enterable))]
public class FloorButton : MonoBehaviour
{
    #region Inspector
    #endregion

    #region Private
    private bool _isPressed;
    #endregion

    #region Public
    public bool IsPressed
    {
        get { return _isPressed; }
        private set
        {
            if(value != _isPressed)
            {
                _isPressed = value;
                MessagingSystem.Instance.TriggerMessage(new FloorButtonPressedMessage(this, _isPressed));
            }
        }
    }
    #endregion

    #region Functions
    // Start is called before the first frame update
    void Start()
    {
        MessagingSystem.Instance.AttachListener(typeof(EnterableTriggeredMessage), OnEnterableTriggered);
    }

    private void OnDestroy()
    {
        MessagingSystem.Instance.DetachListener(typeof(EnterableTriggeredMessage), OnEnterableTriggered);
    }

    private void OnEnterableTriggered(Message message)
    {
        EnterableTriggeredMessage castmsg = message as EnterableTriggeredMessage;

        if(castmsg.enterable.gameObject == gameObject)
        {
            IsPressed = castmsg.isEntered;
        }
    }
    #endregion

    #region Gizmos
    #endregion

    #region Classes
    #endregion
}
