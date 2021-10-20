using System;
using System.Collections;
using UnityEngine;

public class BranchAnimator : MonoBehaviour
{
    public event Action AnimationEnded;

    [SerializeField] private Vector2 _destinationPoint;
    [SerializeField] private float _destinationRotation;
    [SerializeField] private float _animSpeed;

    private Transform _transform;
    private SpriteRenderer _spriteRenderer;

    private void Awake()
    {
        _transform = transform;
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }


    public void PlayCutAnimation(Side side)
    {
        if (side == Side.Left)
            StartCoroutine(TransformMoveAnimation(new Vector2(- _destinationPoint.x, _destinationPoint.y), -_destinationRotation));//dont need change Y value
        else
            StartCoroutine(TransformMoveAnimation(_destinationPoint, _destinationRotation));        
    }

    public void SetSpriteAlpha(float alpha)
    {
        _spriteRenderer.color = new Color(_spriteRenderer.color.r, _spriteRenderer.color.g, _spriteRenderer.color.b, alpha);
    }

    private IEnumerator TransformMoveAnimation(Vector2 destination, float dest_rotation)
    {
        while (_spriteRenderer.color.a > 0.1)
        {
            _transform.localPosition = Vector2.Lerp(_transform.localPosition, destination, _animSpeed * Time.deltaTime);
            _transform.localRotation = Quaternion.Lerp(_transform.localRotation, Quaternion.Euler(0,0, dest_rotation), _animSpeed * Time.deltaTime);
            _spriteRenderer.color = Color.Lerp(_spriteRenderer.color, new Color(_spriteRenderer.color.r, _spriteRenderer.color.g, _spriteRenderer.color.b, 0f), _animSpeed * Time.deltaTime);
            yield return null;
        }
        AnimationEnded?.Invoke();

    }


}
