using UnityEngine;

public class LoadPlayerData
{
    public static int LoadBestScore()
    {
        return PlayerPrefs.GetInt("BestScore", 0);
    }
}
