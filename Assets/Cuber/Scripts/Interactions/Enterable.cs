using System;
using System.Collections;
using System.Collections.Generic;
using TheCuber.Cube;
using UnityEngine;

public class Enterable : MonoBehaviour
{
    #region Inspector
    [SerializeField] private bool _onRollIn = false;
    [SerializeField] private bool _onRollOff = true;
    #endregion

    #region Private
    #endregion

    #region Public
    #endregion

    #region Functions
    private void Awake()
    {
        MessagingSystem.Instance.AttachListener(typeof(CubeMoveStartMessage), OnCubeMoveStart);
        MessagingSystem.Instance.AttachListener(typeof(CubeMoveEndMessage), OnCubeMoveEnd);
    }

    private void OnDestroy()
    {
        MessagingSystem.Instance.DetachListener(typeof(CubeMoveStartMessage), OnCubeMoveStart);
        MessagingSystem.Instance.DetachListener(typeof(CubeMoveEndMessage), OnCubeMoveEnd);
    }

    private void OnCubeMoveStart(Message message)
    {
        CubeMoveStartMessage castmsg = message as CubeMoveStartMessage;

        if (castmsg.toPosition == Vector3Int.RoundToInt(transform.position))
        {
            if (_onRollIn)
            {
                SendMessage(true);
            }
        }

        if (castmsg.fromPosition == Vector3Int.RoundToInt(transform.position))
        {
            if(_onRollOff)
            {
                SendMessage(false);
            }
        }
    }

    private void OnCubeMoveEnd(Message message)
    {
        CubeMoveEndMessage castmsg = message as CubeMoveEndMessage;

        if (castmsg.endPosition == Vector3Int.FloorToInt(transform.position))
        {
            if(!_onRollIn)
            {
                SendMessage(true);
            }
        }

        if (castmsg.fromPosition == Vector3Int.FloorToInt(transform.position))
        {
            if (!_onRollOff)
            {
                SendMessage(false);
            }
        }
    }

    private void SendMessage(bool entered)
    {
        MessagingSystem.Instance.TriggerMessage(new EnterableTriggeredMessage(this, entered));
    }
    #endregion

    #region Gizmos
    #endregion

    #region Classes
    #endregion
}
