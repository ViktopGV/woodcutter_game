using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class GameTimer : MonoBehaviour
{
    public UnityEvent TimeExpired;
    public static event Action TimerStarted;

    [SerializeField] private float _maxGameSeconds = 7;
    [SerializeField] private float _gameSeconds = 4;
    [SerializeField] private Image _fillProgressBar;

    private bool _isCoroutineStarted;

    private void Start()
    {
        Init();
    }

    public void Init()
    {
        _isCoroutineStarted = false;
        _gameSeconds = 4; // REWRITE IT!!
        _fillProgressBar.fillAmount = _gameSeconds / _maxGameSeconds;
    }

    public void StartTimer()
    {
        if (!_isCoroutineStarted && isActiveAndEnabled)
        {
            StartCoroutine(DecreaseTime());
            TimerStarted?.Invoke();
        }

    }

    public void StopTimer()
    {
        if (_isCoroutineStarted)
        {
            StopAllCoroutines();
            _isCoroutineStarted = false;
        }

    }

    public void AddGameSeconds(float seconds)
    {
        if(_gameSeconds < _maxGameSeconds)
            _gameSeconds += seconds;
    }

    private IEnumerator DecreaseTime()
    {
        _isCoroutineStarted = true;
        do
        {
            _gameSeconds -= Time.deltaTime;
            _fillProgressBar.fillAmount = _gameSeconds / _maxGameSeconds;
            yield return null;
        } while (_gameSeconds > 0);

        TimeExpired?.Invoke();
        _isCoroutineStarted = false;
    }

}
