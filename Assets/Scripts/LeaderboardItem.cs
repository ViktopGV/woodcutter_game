using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class LeaderboardItem : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _rank;
    [SerializeField] private Image _avatar;
    [SerializeField] private TextMeshProUGUI _name;
    [SerializeField] private TextMeshProUGUI _score;

    public void SetLeaderboardItem(string rank, string image, string name, string score)
    {
        StartCoroutine(GetAvatarImage(image));
        _rank.text = rank;
        _name.text = name;
        _score.text = score;
    }

    private IEnumerator GetAvatarImage(string url)
    {
        UnityWebRequest www = UnityWebRequestTexture.GetTexture(url);
        yield return www.SendWebRequest();

        Texture2D avatarTexture = DownloadHandlerTexture.GetContent(www);
        _avatar.sprite = Sprite.Create(avatarTexture, new Rect(0,0, avatarTexture.width, avatarTexture.height), Vector2.one / 2);
        _avatar.color = new Color(1, 1, 1, 1);
    }
}
