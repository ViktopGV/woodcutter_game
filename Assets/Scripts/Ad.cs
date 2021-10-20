using System;
using System.Runtime.InteropServices;
using UnityEngine;

public class Ad : MonoBehaviour
{
    [DllImport("__Internal")]
    private static extern void ShowFullScreenAds();

    [DllImport("__Internal")]
    private static extern void ShowRewardAds();

    public event Action Rewarded;

    public void ShowFullScreenAd()
    {
        ShowFullScreenAds();
    }

    public void ShowRewardVideo()
    {
        ShowRewardAds();
    }

    public void RewardAdOverlook()
    {
        Rewarded?.Invoke();
    }
}
