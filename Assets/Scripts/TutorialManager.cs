using UnityEngine;

public class TutorialManager : MonoBehaviour
{
    [SerializeField] private GameTimer _timer;
    [SerializeField] private GameObject _mobilePanel;
    [SerializeField] private GameObject _pcPanel;
    [SerializeField] private GameObject _timePanel;

    private bool _timeTutorialShowed = false;

    private void Start()
    {
        if (YaSDK.GetSafeData("tutorial_pass") == "Null")
            ShowTutorial();
        else _timeTutorialShowed=true;
    }

    private void OnEnable()
    {
        GameTimer.TimerStarted += GameTimer_TimerStarted;        
    }

    private void GameTimer_TimerStarted()
    {
        HidePanels();
    }

    private void OnDisable()
    {
        GameTimer.TimerStarted -= GameTimer_TimerStarted;
    }

    public void ShowTimeTutorial()
    {
        _timePanel.SetActive(true); 
    }

    public void ShowTutorial()
    {
        if(YaSDK.GetDevice() == "desktop")
            _pcPanel.SetActive(true);            
        else
            _mobilePanel.SetActive(true);            

        YaSDK.SetSafeData("tutorial_pass", "true");
    }

    public void HidePanels()
    {
        _mobilePanel.SetActive(false);
        _pcPanel.SetActive(false);
        _timePanel.SetActive(false);
        if (_timeTutorialShowed == false)
        {
            ShowTimeTutorial();
            _timer.StopTimer();
            _timeTutorialShowed = true;
        }
    }
}
