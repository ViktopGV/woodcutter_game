using System.Runtime.InteropServices;
using UnityEngine;

public class TutorialManager : MonoBehaviour
{
    [SerializeField] private PlayerInput _playerInput;

    [SerializeField] private GameObject _mobilePanel;
    [SerializeField] private GameObject _pcPanel;
    [SerializeField] private GameObject _timePanel;

    private Platform _platform;
    private bool _timeTutorialShowed = false;
    private bool _allTutrialsPassed = false;

    private void Start()
    {
        if (YaSDK.GetDevice() == "desktop")
            _platform = Platform.Desktop;
        else
            _platform = Platform.Other;

        if (YaSDK.GetSafeData("tutorial_pass") == "Null")
            ShowTutorial();
        else
        {
            _timeTutorialShowed = true;
            _allTutrialsPassed = true;
            _playerInput.enabled = true;
        }
    }

    private void Update()
    {
        if (_allTutrialsPassed == false)
        {
            if (_platform == Platform.Desktop)
            {
                if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.RightArrow))
                {
                    if (_timeTutorialShowed)
                    {
                        _allTutrialsPassed = true;
                        _playerInput.enabled = true;
                        HidePanels();
                    }
                    HidePanels();                    
                }
            }
            else
            {
                if (Input.touchCount > 0)
                {
                    if (_timeTutorialShowed)
                    {
                        _allTutrialsPassed = true;
                        _playerInput.enabled = true;
                        HidePanels();

                    }
                    HidePanels();                    
                }
            }
        }
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
            _timeTutorialShowed = true;
        }
    }
}
