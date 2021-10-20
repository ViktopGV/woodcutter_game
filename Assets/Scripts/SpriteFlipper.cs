using UnityEngine;

[RequireComponent(typeof(SpriteRenderer), typeof(SideHandler))]
public class SpriteFlipper : MonoBehaviour
{
    private SpriteRenderer _spriteRenderer;
    private SideHandler _sideHandler;

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _sideHandler = GetComponent<SideHandler>();
            
    }

    private void OnEnable()
    {
        _sideHandler.SideSeted += FlipSpriteX;
    }

    private void OnDisable()
    {
        _sideHandler.SideSeted -= FlipSpriteX;
    }

    public void FlipSpriteX()
    {
        if (_sideHandler.Side == Side.Left && _spriteRenderer.flipX == false)
            _spriteRenderer.flipX = true;
        else if (_sideHandler.Side == Side.Right && _spriteRenderer.flipX == true)
            _spriteRenderer.flipX = false;
    }

    public void FlipSpriteY()
    {
        if (_sideHandler.Side == Side.Left && _spriteRenderer.flipY == false)
            _spriteRenderer.flipY = true;
        else if (_sideHandler.Side == Side.Right && _spriteRenderer.flipY == true)
            _spriteRenderer.flipY = false;
    }
}
