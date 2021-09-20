using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial_Button : MonoBehaviour
{
    #region Inspector
    [Header("Pressing")]
    [SerializeField] private bool _respondOnPress;
    [SerializeField] private string _axisName;
    [SerializeField] private bool _positive;
    [SerializeField] private Vector3 _localOffsetOnPress;
    [Header("Text")]
    [SerializeField] private TMPro.TMP_Text _textLetter;
    [SerializeField] private TMPro.TMP_Text _textArrow;
    #endregion

    #region Private
    private Vector3 _localStartPos;
    private float _totalValue = 0;
    #endregion

    #region Public
    #endregion

    #region Functions
    private void Awake()
    {
        _localStartPos = transform.localPosition;

        StartCoroutine(ShuflleText());
    }

    private void Update()
    {
        float value = Mathf.Clamp01(Input.GetAxis(_axisName) * (_positive ? 1 : -1)) ;
        transform.localPosition = Vector3.Lerp(_localStartPos, _localOffsetOnPress, value);
    }

    private IEnumerator ShuflleText()
    {
        _textLetter.color = Color.clear;
        _textArrow.color = Color.white;

        while (true)
        {
            _textArrow.DOColor(Color.white, 1f);
            _textLetter.DOColor(Color.clear, 1f);
            yield return new WaitForSeconds(3);
            _textArrow.DOColor(Color.clear, 1f);
            _textLetter.DOColor(Color.white, 1f);
            yield return new WaitForSeconds(3);
        }
    }
    #endregion

    #region Gizmos
    #endregion

    #region Classes
    #endregion
}
