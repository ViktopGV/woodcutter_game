using UnityEngine;
using UnityEngine.UI;

public class AuthManager : MonoBehaviour
{
    [SerializeField] private AuthBtn _authButton;

    private void OnEnable()
    {
        YaPlayer.PlayerAuthorized += TryShowAuthButton;
    }

    private void OnDisable()
    {
        YaPlayer.PlayerAuthorized -= TryShowAuthButton;

    }

    public void TryShowAuthButton()
    {
        _authButton.gameObject.SetActive(!YaPlayer.IsPlayerAuthorized);
        _authButton.SuccsessAuth();
    }
}
