using UnityEngine;

public class RevivialLimiter : MonoBehaviour
{
    private int _rewardedCount;

    void Start()
    {
        _rewardedCount = 0;
    }

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
        if(_rewardedCount >= 3)
            gameObject.SetActive(false);
        _rewardedCount++;
    }
}
