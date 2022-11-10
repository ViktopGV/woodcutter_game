using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AuthBtn : MonoBehaviour
{
    [SerializeField] private TMPro.TextMeshProUGUI _authText;
    [SerializeField] private TMPro.TextMeshProUGUI _succsessAuthText;
    [SerializeField] private Image _succsessImage;

    public void SuccsessAuth()
    {
        if (YaPlayer.IsPlayerAuthorized)
        {
            _authText.gameObject.SetActive(false);
            _succsessAuthText.gameObject.SetActive(true);
            _succsessImage.gameObject.SetActive(true);
        }
    }
}
