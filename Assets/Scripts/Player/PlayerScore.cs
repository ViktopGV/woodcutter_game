using System;
using System.Runtime.InteropServices;
using UnityEngine;


public class PlayerScore : MonoBehaviour
{
    public LeaderboardManager LeaderboardManager;

    public event Action<int> ScoreIncreased;
    public int Score => _score;
    public int BestScore => _bestScore;

    private int _score;
    private int _bestScore;

    struct BS { public int bestScore; }
    private BS _bs;

    private void OnEnable()
    {
        YaPlayer.PlayerGetData += YaPlayer_PlayerGetData; 
    }

    private void OnDisable()
    {
        YaPlayer.PlayerGetData -= YaPlayer_PlayerGetData;
    }

    private void YaPlayer_PlayerGetData(string obj)
    {
        _bs = JsonUtility.FromJson<BS>(obj);
        _bestScore = _bs.bestScore;        
    }

    public void LoadBestScore()
    {
        if(YaPlayer.IsPlayerAuthorized)
        {
            if (int.TryParse(YaSDK.GetSafeData("bestScore"), out _bestScore) == true)
            {
                _bs.bestScore = _bestScore;
                YaPlayer.SetData(JsonUtility.ToJson(_bs));
                YaSDK.RemoveItem("bestScore");
            }
            else
            {
                YaPlayer.GetData();
            }
        }
        else
        {
            if (int.TryParse(YaSDK.GetSafeData("bestScore"), out _bestScore) == false)
                _bestScore = 0;
        }

        
    }

    private void Start()
    {
        _bs = new BS();
        LoadBestScore();
    }

    public void IncreaseScore()
    {
        _score++;
        ScoreIncreased?.Invoke(_score);
    }

    public bool IsBestScoreCheck()
    {
        if (_bestScore < _score)
            return true;
        else
            return false;
    }

    public void SetToZero()
    {
        _bs.bestScore = 0;
        YaPlayer.SetData(JsonUtility.ToJson(_bs));
    }

    public void SaveBestScore()
    {
        if (IsBestScoreCheck())
        {
            _bs.bestScore = _bestScore = _score;
            if (YaPlayer.IsPlayerAuthorized)
            {
                YaPlayer.SetData(JsonUtility.ToJson(_bs));
                YaLeaderboard.SetLeaderboardScore("BestScore", _bs.bestScore);
            }
            else
            {
                YaSDK.SetSafeData("bestScore", _bestScore.ToString());
            }

        }
        else
            LeaderboardManager.LoadLeaderboard();
        
    }
}
