using System.Runtime.InteropServices;
using UnityEngine;

public class YaSDK : MonoBehaviour
{
    [DllImport("__Internal")]
    public static extern string GetDevice();
    [DllImport("__Internal")]
    public static extern void SetSafeData(string key, string value);
    [DllImport("__Internal")]
    public static extern string GetSafeData(string key);
    [DllImport("__Internal")]
    public static extern void RemoveItem(string item_key);
}
