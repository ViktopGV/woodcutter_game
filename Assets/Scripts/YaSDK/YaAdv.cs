using System;
using System.Runtime.InteropServices;
using UnityEngine;

public class YaAdv : MonoBehaviour
{
    public static event Action FullscreenOpened;
    public static event Action<bool> FullscreenClosed;
    public static event Action<string> FullscreenError;
    public static event Action FullscreenOffline;

    public static event Action<int> RewardOpened;
    public static event Action<int> Rewarded;
    public static event Action<int, string> RewardError;
    public static event Action<int> RewardClosed;

    [DllImport("__Internal")]
    public static extern void ShowFullscreenAdv();
    [DllImport("__Internal")]
    public static extern void ShowRewardAdv(int id);


    private void FullscreenAdvOpenedCallback() => FullscreenOpened?.Invoke();
    private void FullscreenAdvClosedCallback(int wasShow) => FullscreenClosed?.Invoke(Convert.ToBoolean(wasShow));
    private void FullscreenAdvErrorCallback(string error) => FullscreenError?.Invoke(error);
    private void FullscreenAdvOfflineCallback() => FullscreenOffline?.Invoke();
    private void RewardAdvOpenCallback(int id) => RewardOpened?.Invoke(id);
    private void RewardedCallback(int id) => Rewarded?.Invoke(id);
    private void RewardAdvClosedCallback(int id) => RewardClosed?.Invoke(id);
    private void RewardAdvErrorCallback(int id, string error) => RewardError?.Invoke(id, error);
}
