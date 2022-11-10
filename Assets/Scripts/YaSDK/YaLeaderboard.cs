using Leaderboard;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class YaLeaderboard : MonoBehaviour
{
    public static event Action<LeaderboardEntries> GotLeaderboardEntries;
    public static event Action LeaderboardScoreSetted;
    [DllImport("__Internal")]
    public static extern void GetLeaderboardEntries(string leaderboard, bool includeUser = false, int quantityAround = 5, int quantityTop = 5, string avatarSize = "small");

    [DllImport("__Internal")]
    public static extern void SetLeaderboardScore(string leaderboard, float score);
    [DllImport("__Internal")]
    private static extern void ItitializeLeaderboard();

    private void Start()
    {
        ItitializeLeaderboard();
    }

    private void GetLeaderboardEntriesCallback(string json)
    {
        LeaderboardEntries lb = JsonUtility.FromJson<LeaderboardEntries>(json);
        GotLeaderboardEntries?.Invoke(lb);
    }

    private void SetLeaderboardCallback()
    {
        LeaderboardScoreSetted?.Invoke();
    }
}

namespace Leaderboard
{

    [Serializable]
    public struct Description
    {
        public bool invert_sort_order;
        public ScoreFormat score_format;
        public string type;
    }

    [Serializable]
    public struct Entry
    {
        public int score;
        public string extraData;
        public int rank;
        public Player player;
        public string formattedScore;
    }

    [Serializable]
    public struct Leaderboard
    {
        public string appID;
        public bool @default;
        public Description description;
        public string name;
        public Title title;
    }

    [Serializable]
    public struct Options
    {
        public int decimal_offset;
    }

    [Serializable]
    public struct Player
    {
        public string avatarSrc;
        public string avatarSrcSet;
        public string lang;
        public string publicName;
        public ScopePermissions scopePermissions;
        public string uniqueID;
    }

    [Serializable]
    public struct Range
    {
        public int start;
        public int size;
    }

    [Serializable]
    public struct LeaderboardEntries
    {
        public Leaderboard leaderboard;
        public List<Range> ranges;
        public int userRank;
        public List<Entry> entries;
    }

    [Serializable]
    public struct ScopePermissions
    {
        public string avatar;
        public string public_name;
    }

    [Serializable]
    public struct ScoreFormat
    {
        public Options options;
    }

    [Serializable]
    public struct Title
    {
        public string en;
        public string ru;
        public string be;
        public string uk;
        public string kk;
        public string uz;
        public string tr;
    }

}