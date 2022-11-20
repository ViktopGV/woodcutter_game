using UnityEngine;
using UnityEngine.UI;

public class AuthManager : MonoBehaviour
{
    [SerializeField] private AuthBtn _authButton;

    private bool alreadyAuth = false;

    private void OnEnable()
    {   
        _authButton.gameObject.SetActive(false);
        YaPlayer.PlayerAuthorized += ShowSuccsesAuthButton;
        YaPlayer.PlayerInited += YaPlayer_PlayerInited;
    }

    private void YaPlayer_PlayerInited(bool obj)
    {
        alreadyAuth = obj;
        _authButton.gameObject.SetActive(!YaPlayer.IsPlayerAuth());
    }

    private void OnDisable()
    {
        YaPlayer.PlayerAuthorized -= ShowSuccsesAuthButton;
        YaPlayer.PlayerInited -= YaPlayer_PlayerInited;
    }

    public void ShowSuccsesAuthButton()
    {
        if (alreadyAuth == false)
            _authButton.SuccsessAuth();
    }
}
