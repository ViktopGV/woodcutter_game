using System.Collections;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class PlayerAnimation : MonoBehaviour
{
    [SerializeField] private Sprite _attackSprite;
    [SerializeField] private Sprite _idleSprite;
    [SerializeField] private Sprite _diedSprite;
    [SerializeField] private float _changeSpriteSpeed = .1f;

    private SpriteRenderer _spriteRenederer;

    private void Awake()
    {
        _spriteRenederer = GetComponent<SpriteRenderer>();
    }

    public void SetIdleSprite()
    {
        SetSprite(_idleSprite);
    }

    public void SetAttackSprite()
    {
        SetSprite(_attackSprite);
        StartCoroutine(BackIdleSprite(_changeSpriteSpeed));
    }

    public void SetDiedSprite()
    {
        SetSprite(_diedSprite);
        StopAllCoroutines();
    }

    private IEnumerator BackIdleSprite(float backSpeed)
    {
        yield return new WaitForSeconds(backSpeed);
        SetIdleSprite();
    }

    private void SetSprite(Sprite sprite)
    {
        _spriteRenederer.sprite = sprite;
    }
}
