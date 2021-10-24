using System;
using System.Runtime.InteropServices;
using UnityEngine;


public class PlayerScore : MonoBehaviour
{
    [DllImport("__Internal")]
    private static extern bool AppleDeviceCheck();

    [DllImport("__Internal")]
    private static extern void SetData(string key, int value);

    [DllImport("__Internal")]
    private static extern int GetData(string key);


    public event Action<int> ScoreIncreased;
    public int Score => _score;
    public int BestScore => _bestScore;

    private int _score;
    private int _bestScore;
    private bool isAppleDevice;

    private void Awake()
    {
        isAppleDevice = AppleDeviceCheck();
    }

    private void Start()
    {
        _score = AdRevival.GetScore();
        if (isAppleDevice)
        {
            //не работает, это бутофория, надо просто вызвать GetData
            _bestScore = GetData("score");
        }
        else
        {
            _bestScore = LoadPlayerData.LoadBestScore();
        }
    }

    //будет вызван из js для загрузки bestScore из storage яндекса 
    private void LoadBestScore(int score)
    {
        _bestScore = score;
    }

    public void IncreaseScore()
    {
        _score++;
        ScoreIncreased?.Invoke(_score);
        if (IsBestScoreCheck())
            SaveBestScore();
    }

    public bool IsBestScoreCheck()
    {
        if (_bestScore < _score)
            return true;
        else
            return false;
    }

    public void SaveBestScore()
    {
        SavePlayerData.SaveBestScore(_score);
        _bestScore = _score;
        if (isAppleDevice)
            SetData("score", _bestScore);
    }
}
