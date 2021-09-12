using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuController : MonoBehaviour
{
    #region Inspector
    [SerializeField] private Button _buttonNewGame;
    [SerializeField] private Button _buttonSelectLevel;
    [SerializeField] private Button _buttonExit;
    #endregion

    #region Private
    #endregion

    #region Public
    #endregion

    #region Functions
    private void Awake()
    {
        _buttonNewGame.onClick.AddListener(OnClickNewGame);
        _buttonSelectLevel.onClick.AddListener(OnClickSelectLevel);
        _buttonExit.onClick.AddListener(OnClickExit);
        FadeController.Instance.Fade(false, 1f);
    }

    private void OnClickNewGame()
    {
        SceneLoader.Instance.LoadLevel(1);
    }

    private void OnClickSelectLevel()
    {
        throw new NotImplementedException();
    }

    private void OnClickExit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
        Application.Quit();
    }
    #endregion

    #region Gizmos
    #endregion

    #region Classes
    #endregion
}
