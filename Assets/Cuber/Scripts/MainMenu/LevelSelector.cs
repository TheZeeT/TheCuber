using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelSelector : MonoBehaviour
{
    #region Inspector
    [SerializeField] private GameObject _ButtonPrefab;
    [SerializeField] private Transform _buttonHolder;
    [SerializeField] private Button _buttonClose;
    #endregion

    #region Private
    #endregion

    #region Public
    #endregion

    #region Functions
    private void Awake()
    {
        CreateButtons(SceneLoader.Instance.TotalLevelCount);
        _buttonClose.onClick.AddListener(CloseLevelSelector);
    }

    private void CloseLevelSelector()
    {
        PauseMenu.Instance.ShowLevelSelector(false);
        PauseMenu.Instance.ShowPauseMenu(false);
    }

    private void CreateButtons(int count)
    {
        for (int i = 0; i < count; i++)
        {
            GameObject newParant = Instantiate(_ButtonPrefab, _buttonHolder);
            newParant.gameObject.SetActive(true);
            Button newButton = newParant.GetComponentInChildren<Button>();
            int id = i + 1;
            newButton.onClick.AddListener(() => LoadLevel(id));
            newButton.gameObject.name = $"{newButton.gameObject.name} #{id}";
            TMPro.TMP_Text buttonText = newButton.GetComponentInChildren<TMPro.TMP_Text>();
            buttonText.text = $"{id}";
        }
    }

    public void LoadLevel(int levelNumber)
    {
        Debug.Log($"Loading Level {levelNumber}");
        SceneLoader.Instance.LoadLevel(levelNumber);
        CloseLevelSelector();
    }
    #endregion

    #region Gizmos
    #endregion

    #region Classes
    #endregion
}
