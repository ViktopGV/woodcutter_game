using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zero : MonoBehaviour
{
    public void SetToZero() => YaLeaderboard.SetLeaderboardScore("BestScore", 0);
}
