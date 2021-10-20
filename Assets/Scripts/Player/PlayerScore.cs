using System;
using UnityEngine;

public class PlayerScore : MonoBehaviour
{
    public event Action<int> ScoreIncreased;
    public int Score => _score;
    public int BestScore => _bestScore;

    private int _score;
    private int _bestScore;

    private void Start()
    {
        _score = AdRevival.GetScore();
        _bestScore = LoadPlayerData.LoadBestScore();
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
    }
}
