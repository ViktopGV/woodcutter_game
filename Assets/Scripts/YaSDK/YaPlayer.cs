using System;
using System.Runtime.InteropServices;
using UnityEngine;

public class YaPlayer : MonoBehaviour
{
    public static event Action<bool> PlayerInited;
    public static event Action PlayerAuthorized;
    public static event Action PlayerRefusedAuthorize;
    public static event Action<string> PlayerIncrementedStats;
    public static event Action<string> PlayerGetIncrementedStats;
    public static event Action<string> PlayerGetData;


    [DllImport("__Internal")]
    public static extern void OpenAuthDialog();
    [DllImport("__Internal")]
    public static extern void IncrementStats(string data);
    [DllImport("__Internal")]
    public static extern void SetData(string data, bool flush = false);
    [DllImport("__Internal")]
    public static extern void GetData();
    [DllImport("__Internal")]
    public static extern bool IsPlayerAuth();


    [DllImport("__Internal")]
    private static extern void InitializationPlayer();

    void Start()
    {
        InitializationPlayer();
    }

    private void PlayerSuccsessfullyInitedCallback(int isAuth) 
    {
        PlayerInited?.Invoke(!Convert.ToBoolean(isAuth));
    }
    private void PlayerSuccsessfullyAuthCallback()
    {
        PlayerAuthorized?.Invoke();
    }
    private void PlayerRefuseAuthCallback()
    {
        PlayerRefusedAuthorize?.Invoke();
    }

    private void PlayerIncrementStatsCallback(string result) => PlayerIncrementedStats?.Invoke(result);
    private void PlayerGetStatsCallback(string result) => PlayerGetIncrementedStats?.Invoke(result);
    private void PlayerGetDataCallback(string result) => PlayerGetData?.Invoke(result);

}
