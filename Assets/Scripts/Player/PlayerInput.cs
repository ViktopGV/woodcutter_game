using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.Events;

public enum Platform
{
    Desktop, 
    Other
}

public class PlayerInput : MonoBehaviour
{
    [DllImport("__Internal")]
    private static extern bool YandexPlatformCheck();

    public UnityEvent LeftClicked;
    public UnityEvent RightClicked;

    private Platform _platform;
    private float _halfScreenWidth;

    private void Start()
    {
        _platform = Platform.Desktop;
        _halfScreenWidth = Screen.width / 2;
#if !UNITY_EDITOR
        if (YandexPlatformCheck())
            _platform = Platform.Desktop;
        else
            _platform = Platform.Other;
#else 
        _platform = Platform.Desktop;
#endif
    }

    private void Update()
    {
        if (_platform == Platform.Desktop)
        {            
            if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                LeftClicked?.Invoke();
            }
            else if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                RightClicked?.Invoke();
            }
        }
        else
        {
            Touch touch;
            if(Input.touchCount > 0)
            {
                touch = Input.GetTouch(0);
                if (touch.phase == TouchPhase.Ended && touch.position.x < _halfScreenWidth)
                    LeftClicked?.Invoke();
                else if (touch.phase == TouchPhase.Ended && touch.position.x > _halfScreenWidth)
                    RightClicked?.Invoke();
            }
        }

    }
}
