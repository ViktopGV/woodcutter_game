using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AdRevival : MonoBehaviour
{
    [SerializeField] private Button _revivalButton;
    [SerializeField] private float _adTimer = 180;
    [SerializeField] private int _attemptCount = 3;

    //из-за того что при возрождении будет перегрузка уровня, надо сохранить количество очков
    private static int _playerScore = 0;//
    private static float _remainingTime = 0;
    private static List<Coroutine> _coroutines;

    public static int GetScore() => _playerScore;
    public static void SetScore(int score) => _playerScore = score;

    private void Awake()
    {
        _coroutines ??= new List<Coroutine>();
    }

    private void Start()
    {

        if (_remainingTime <= 0)
            _remainingTime = _adTimer;

        if(_coroutines.Count > 0)
        {
            for (int i = 0; i < _coroutines.Count; i++)
            {
                _coroutines[i] = StartCoroutine(UpdateAdCounter());
            }
        }
    }

    public void RewardAdCheck()
    {
        if (_coroutines.Count >= _attemptCount)
            _revivalButton.gameObject.SetActive(false);
        else
            _revivalButton.gameObject.SetActive(true);
    }

    private void Update()
    {
        if(_coroutines.Count != 0)
        {
            _remainingTime -= Time.unscaledDeltaTime;
        }
    }

    public void IncreaseAdShowedCount()
    {
        _coroutines.Add(StartCoroutine(UpdateAdCounter()));
    }

    private IEnumerator UpdateAdCounter()
    {
        yield return new WaitForSecondsRealtime(_remainingTime);
        _coroutines.RemoveAt(0);
        RewardAdCheck();
    }
}
