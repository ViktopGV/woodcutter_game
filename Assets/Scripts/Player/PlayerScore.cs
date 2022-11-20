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

    private void OnEnable()
    {
        YaPlayer.PlayerGetData += YaPlayer_PlayerGetData;
        YaPlayer.PlayerInited += YaPlayer_PlayerInited;
        YaPlayer.PlayerAuthorized += YaPlayer_PlayerAuthorized;
        YaLeaderboard.GotLeaderboardPlayerEntry += YaLeaderboard_GotLeaderboardPlayerEntry;
    }

    private void YaPlayer_PlayerAuthorized()
    {
        YaLeaderboard.GetLeaderboardPlayerEntry("BestScore");
    }

    private void YaLeaderboard_GotLeaderboardPlayerEntry(Leaderboard.Entry obj)
    {
        if (_bestScore > obj.score)
            YaLeaderboard.SetLeaderboardScore("BestScore", _bestScore);
    }

    private void YaPlayer_PlayerInited(bool obj)
    {
        LoadBestScore();
    }

    private void OnDisable()
    {
        YaPlayer.PlayerInited -= YaPlayer_PlayerInited;
        YaPlayer.PlayerGetData -= YaPlayer_PlayerGetData;
        YaPlayer.PlayerAuthorized -= YaPlayer_PlayerAuthorized;

        YaLeaderboard.GotLeaderboardPlayerEntry -= YaLeaderboard_GotLeaderboardPlayerEntry;

    }

    private void YaPlayer_PlayerGetData(string obj)
    {
        BS bs = JsonUtility.FromJson<BS>(obj);
        _bestScore = bs.bestScore;        
    }

    public void LoadBestScore()
    {
        
        string score = YaSDK.GetSafeData("bestScore");
        if (YaPlayer.IsPlayerAuth() == true)
        {
            if (score != "Null")
            {
                BS bs = new BS();
                bs.bestScore = int.Parse(score);
                _bestScore = bs.bestScore;
                YaPlayer.SetData(JsonUtility.ToJson(bs));
                SaveBestScore();
                YaSDK.RemoveItem("bestScore");

            }
            else
            {
                YaPlayer.GetData();
            }
        }
        else
        {
            if (score != "Null")
            {
                _bestScore = int.Parse(score);
            }
            else
            {
                _bestScore = 0;
            }
        }

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

    public void SaveBestScore()
    {
        if (IsBestScoreCheck() == true)
        {
            _bestScore = _score;

            if(YaPlayer.IsPlayerAuth() == true)
            {
                BS bs = new BS();
                bs.bestScore = _bestScore;
                YaPlayer.SetData(JsonUtility.ToJson(bs));
                YaLeaderboard.SetLeaderboardScore("BestScore", _bestScore);
            }
            else
            {
                YaSDK.SetSafeData("bestScore", _bestScore.ToString());
            }
        }
    }
}