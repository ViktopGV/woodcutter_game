using System;
using UnityEngine;

public enum Side
{
    Right,
    Left
}

public class SideHandler : MonoBehaviour
{
    public event Action SideSeted;
    public Side Side => _side;
    public float SideCoordinate
    {
        get
        {
            if (_side == Side.Left)
                return _leftSide;
            else if (_side == Side.Right)
                return _rightSide;
            return -1;
        }
        private set { }


    }

    [SerializeField] private float _leftSide;
    [SerializeField] private float _rightSide;

    private Side _side;
    private Transform _transform;

    private void Awake()
    {
        _transform = transform;
    }

    public void SetRandomSide()
    {
        int side = UnityEngine.Random.Range(0, 2);
        if (side == 0)        
            SetOnLeftSide();        
        else
            SetOnRightSide();
    }

    public void SetOnLeftSide()
    {
        SetTranformOnSide(Side.Left);
    }

    public void SetOnRightSide()
    {
        SetTranformOnSide(Side.Right);
    }

    private void SetTranformOnSide(Side side)
    {
        if (side == Side.Left)
        {
            _transform.localPosition = new Vector2(_leftSide, _transform.localPosition.y);
            _side = Side.Left;
        }
        else if (side == Side.Right)
        {
            _transform.localPosition = new Vector2(_rightSide, _transform.localPosition.y);
            _side = Side.Right;
        }
        SideSeted?.Invoke();
    }
}
