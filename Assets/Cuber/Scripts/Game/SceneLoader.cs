using System;
using System.Collections;
using System.Collections.Generic;
using TheCuber.Cube;
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
    private int _currentLevel = -1;
    #endregion

    #region Public
    public static SceneLoader Instance { get; private set; }
    public int TotalLevelCount { get { return _sceneNames.Length; } }
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
        //FadeController.Instance.Fade(false, 1f);
    }

    private void LoadScene(string name)
    {
        //if (_loadedScenes.Contains(name))
        //{
        //    Debug.LogError($"Scene <b>{name}</b> already Loaded");
        //}
        //else
        //{
        SceneManager.LoadSceneAsync(name, LoadSceneMode.Additive);
        _loadedScenes.Add(name);
        //}
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

    public void LoadLevel(int number)
    {
        StartCoroutine(ChangeScene(number));
    }

    public void LoadNextLevel()
    {
        LoadLevel(_currentLevel + 1);
    }

    public void ReloadLevel()
    {
        LoadLevel(_currentLevel);
    }

    private IEnumerator ChangeScene(int number)
    {
        FadeController.Instance.Fade(true, 1f);
        yield return new WaitForSeconds(1.5f);

        UnloadScene(_loadedScenes[0]);

        yield return new WaitForSeconds(1.5f);

        if (number > 0 && number - 1 < _sceneNames.Length)
        {
            LoadScene(_sceneNames[number - 1]);
            _currentLevel = number;
        }
        else
        {
            LoadScene(_mainMenuName);
            _currentLevel = -1;
            CameraController.Instace.EnableCamera(false);
        }
    }
    #endregion

    #region Gizmos
    #endregion

    #region Classes
    #endregion
}
