using UnityEngine;

public class AdPromt : MonoBehaviour
{
    [SerializeField] private GameObject _promtObject;
    [SerializeField] private Animator _animator;

    private void OnEnable()
    {
        if(YaSDK.GetSafeData("advice") == "Null")
        {
            _animator.SetBool("Animating", true);
            YaSDK.SetSafeData("advice", "true");
        }
        else
        {
            _promtObject.SetActive(false);
        }

    }
}
