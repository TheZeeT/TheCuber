using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    #region Inspector
    [SerializeField] private GameObject _menuBG;
    [SerializeField] private GameObject _menu;
    [SerializeField] private GameObject _levelSelector;
    [Header("Buttons")]
    [SerializeField] private Button _buttonContinue;
    [SerializeField] private Button _buttonLevelSelector;
    [SerializeField] private Button _buttonMainMenu;
    #endregion

    #region Private
    #endregion

    #region Public
    public static PauseMenu Instance { get; private set; }
    public bool PauseMenuActive { get; private set; }
    public bool LevelSelectorActive { get; private set; }
    #endregion

    #region Functions
    private void Awake()
    {
        Instance = this;
        _buttonContinue.onClick.AddListener(OnButtonContinue);
        _buttonLevelSelector.onClick.AddListener(OnButtonLevelSelector);
        _buttonMainMenu.onClick.AddListener(OnButtonMainMenu);

        ShowPauseMenu(false);
        ShowLevelSelector(false);
    }

    private void OnButtonContinue()
    {
        ShowPauseMenu(false);
    }

    private void OnButtonLevelSelector()
    {
        ShowLevelSelector(!LevelSelectorActive);
    }

    private void OnButtonMainMenu()
    {
        SceneLoader.Instance.LoadLevel(-1);
        ShowPauseMenu(false);
    }

    public void ShowPauseMenu(bool show)
    {
        if (FadeController.Instance.IsBlack)
            show = false;

        PauseMenuActive = show;

        _menu.SetActive(show);

        ShowMenuBG();
    }

    public void ShowLevelSelector(bool show)
    {
        if (FadeController.Instance.IsBlack)
            show = false;

        LevelSelectorActive = show;

        _levelSelector.SetActive(show);

        ShowMenuBG();
    }

    private void ShowMenuBG()
    {
        _menuBG.SetActive(_menu.activeSelf || _levelSelector.activeSelf);
    }
    #endregion

    #region Gizmos
    #endregion

    #region Classes
    #endregion
}
