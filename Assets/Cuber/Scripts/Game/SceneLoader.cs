using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    #region Inspector
    [SerializeField] private string _mainMenuName;
    [SerializeField] private string[] _sceneNames;
    #endregion

    #region Private
    private List<string> _loadedScenes;
    #endregion

    #region Public
    public static SceneLoader Instance { get; private set; }
    #endregion

    #region Functions
    private void Awake()
    {
        Instance = this;
        GameObject.DontDestroyOnLoad(this.gameObject);
        _loadedScenes = new List<string>();

        SceneManager.sceneLoaded += OnSceneLoaded;

        LoadScene(_mainMenuName);
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode loadSceneMode)
    {
        FadeController.Instance.Fade(false, 1f);
    }

    public void LoadLevel(int number)
    {
        LoadScene(_sceneNames[number - 1]);
    }

    private void LoadScene(string name)
    {
        if (_loadedScenes.Contains(name))
        {
            Debug.LogError($"Scene <b>{name}</b> already Loaded");
        }
        else
        {
            SceneManager.LoadSceneAsync(name, LoadSceneMode.Additive);
            _loadedScenes.Add(name);
        }
    }

    private void UnloadScene(string name)
    {
        if (_loadedScenes.Contains(name))
        {
            SceneManager.UnloadSceneAsync(name, UnloadSceneOptions.UnloadAllEmbeddedSceneObjects);
            _loadedScenes.Remove(name);
        }
        else
            Debug.LogError($"No scene <b>{name}</b> is loaded");
    }
    #endregion

    #region Gizmos
    #endregion

    #region Classes
    #endregion
}
