using UnityEngine;
using UnityEngine.Events;

public class RevivalManager : MonoBehaviour
{
    public UnityEvent AdvRewarded;

    private void OnEnable()
    {
        YaAdv.Rewarded += YaAdv_Rewarded;
    }

    private void OnDisable()
    {
        YaAdv.Rewarded -= YaAdv_Rewarded;
    }

    private void YaAdv_Rewarded(int obj)
    {
        if(obj == 1)
        {
            AdvRewarded?.Invoke();
        }
    }
}
