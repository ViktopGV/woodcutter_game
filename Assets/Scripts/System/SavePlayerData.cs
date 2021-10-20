using UnityEngine;

public static class SavePlayerData
{
    public static void SaveBestScore(int bestScoreValue)
    {
        PlayerPrefs.SetInt("BestScore", bestScoreValue);
    }
}

