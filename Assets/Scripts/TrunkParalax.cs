using UnityEngine;

public class TrunkParalax : MonoBehaviour
{
    [SerializeField] private Transform nextTransform;
    private Transform _transofrm;
    private float _spriteHeight;

    private void Awake()
    {
        _spriteHeight = GetComponent<SpriteRenderer>().bounds.size.y;
        _transofrm = transform;
    }

    public void UpdateParalax()
    {
        _transofrm.localPosition = new Vector2(0, nextTransform.localPosition.y + _spriteHeight);
    }

    private void OnBecameInvisible()
    {
        UpdateParalax();
    }
}
