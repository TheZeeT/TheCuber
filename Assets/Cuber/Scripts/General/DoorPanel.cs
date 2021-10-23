using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorPanel : MonoBehaviour
{
    #region Inspector
    [SerializeField] private FloorButton _button;
    [SerializeField] private float _fadeDuration = 1f;
    [SerializeField] private Renderer[] _renderers;
    [SerializeField] private Collider[] _colliders;
    #endregion

    #region Private
    private bool _isOpen;
    #endregion

    #region Public
    #endregion

    #region Functions
    private void Awake()
    {
        MessagingSystem.Instance.AttachListener(typeof(FloorButtonPressedMessage), OnFloorButtonPressed);
        SetState(false, true);
    }

    private void OnDestroy()
    {
        MessagingSystem.Instance.DetachListener(typeof(FloorButtonPressedMessage), OnFloorButtonPressed);
    }

    private void OnFloorButtonPressed(Message message)
    {
        FloorButtonPressedMessage castmsg = message as FloorButtonPressedMessage;

        if (castmsg.button.gameObject == _button.gameObject)
        {
            if (castmsg.isPressed)
                SetState(!_isOpen);
        }
    }

    private void SetState(bool isActive, bool isIsntant = false)
    {
        _isOpen = isActive;

        for (int i = 0; i < _renderers.Length; i++)
            _renderers[i].material.DOFloat(_isOpen ? 0 : 1, "_Alpha", isIsntant ? 0f : _fadeDuration);

        for (int i = 0; i < _colliders.Length; i++)
            _colliders[i].enabled = !_isOpen;

    }
    #endregion

    #region Gizmos
    #endregion

    #region Classes
    #endregion
}
