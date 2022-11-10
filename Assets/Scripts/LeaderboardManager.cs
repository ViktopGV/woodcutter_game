using System.Collections;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class LeaderboardManager : MonoBehaviour
{
    [SerializeField] private PlayerScore _playerScore;
    public LeaderboardItem _prefab;
    private LeaderboardItem[] _items;
    private int _itemsCount = 5;


    private void Start()
    {
        _items = new LeaderboardItem[_itemsCount];
        for(int i = 0; i < _itemsCount; i++)
        {
            _items[i] = Instantiate(_prefab, transform);
        }
    }

    public void LoadLeaderboard()
    {
        if(YaPlayer.IsPlayerAuthorized == true)
            YaLeaderboard.GetLeaderboardEntries("BestScore", true, 3, 1);
        else
            YaLeaderboard.GetLeaderboardEntries("BestScore", false, 1, 5);

    }


    private void OnEnable()
    {
        YaLeaderboard.GotLeaderboardEntries += YaLeaderboard_GotLeaderboardEntries;
        YaLeaderboard.LeaderboardScoreSetted += YaLeaderboard_LeaderboardScoreSetted;
    }




    private void YaLeaderboard_LeaderboardScoreSetted()
    {
        LoadLeaderboard();
        Debug.Log("Установили лидерборд");
    }

    private void OnDisable()
    {
        YaLeaderboard.GotLeaderboardEntries -= YaLeaderboard_GotLeaderboardEntries;
        YaLeaderboard.LeaderboardScoreSetted -= YaLeaderboard_LeaderboardScoreSetted;

    }

    private void YaLeaderboard_GotLeaderboardEntries(Leaderboard.LeaderboardEntries obj)
    {
        Debug.Log("Получили лидерборд");
        int i = 0;
        if (obj.entries.Count < _itemsCount)
        {
            foreach (var o in obj.entries)
            {
                _items[i].SetLeaderboardItem(o.rank.ToString(), o.player.avatarSrc, o.player.publicName == string.Empty ? "Anonim" + o.player.uniqueID[0..5] : o.player.publicName, o.score.ToString());
                ++i;
            }
        }
        else
        {
            for(int j = obj.entries.Count - _itemsCount; j < obj.entries.Count; ++j)
            {
                _items[i].SetLeaderboardItem(obj.entries[j].rank.ToString(), obj.entries[j].player.avatarSrc, obj.entries[j].player.publicName == string.Empty ? "Anonim" + obj.entries[j].player.uniqueID[0..5] : obj.entries[j].player.publicName, obj.entries[j].score.ToString());
                ++i;
            }
        }

    }

    
}
