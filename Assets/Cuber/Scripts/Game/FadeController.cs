using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TheCuber.Cube;
using UnityEngine;
using UnityEngine.UI;

public class FadeController : MonoBehaviour
{
    #region Inspector
    [SerializeField] private Image _blackScreen;
    #endregion

    #region Private
    #endregion

    #region Public
    public static FadeController Instance { get; private set; }
    #endregion

    #region Functions
    private void Awake()
    {
        Instance = this;
        _blackScreen.color = Color.black;
        GameObject.DontDestroyOnLoad(this.gameObject);
    }

    public void Fade(bool toBlack, float duration)
    {
        if (toBlack)
        {
            _blackScreen.gameObject.SetActive(true);
            BlockOnFade(true);
        }
        _blackScreen.DOColor(toBlack ? Color.black : Color.clear, duration)
            .OnComplete(() => OnFadeEnd(toBlack));
    }

    private void OnFadeEnd(bool toBlack)
    {
        _blackScreen.gameObject.SetActive(toBlack);
        BlockOnFade(toBlack);
    }

    private void BlockOnFade(bool block)
    {
        CubeController.Instance.AddOrRemoveBlocker("ScreenIsBlock", gameObject, block);
    }
    #endregion

    #region Gizmos
    #endregion

    #region Classes
    #endregion
}
