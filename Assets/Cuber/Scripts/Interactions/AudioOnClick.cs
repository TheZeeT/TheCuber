using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(AudioSource))]
public class AudioOnClick : MonoBehaviour
{
    #region Inspector
    [SerializeField] private Button _button;
    [SerializeField] private AudioSource _audio;
    #endregion

    #region Private
    #endregion

    #region Public
    #endregion

    #region Functions
    private void Awake()
    {
        _button.onClick.AddListener(OnClick);
    }

    private void OnClick()
    {
        _audio.Play();
    }
    #endregion

    #region Gizmos
    #endregion

    #region Classes
    #endregion
}
