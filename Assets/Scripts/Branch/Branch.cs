using System;
using UnityEngine;

public class Branch : MonoBehaviour
{
    public event Action<Branch> BranchCut;

    public Side BranchSide => _branchSide.Side;
    public Transform Transofrm => _transform;
    public BranchAnimator Anim => _branchAnim;

    private Transform _transform;
    private SideHandler _branchSide;
    private Mover _mover;
    private PlayerInput _input;
    private BranchAnimator _branchAnim;
    

    private void Awake()
    {
        _transform = transform;
        _branchSide = GetComponent<SideHandler>();
        _input = Player.Instance.GetComponent<PlayerInput>();
        _mover = GetComponent<Mover>();
        _branchAnim = GetComponent<BranchAnimator>();
    }

    private void OnEnable()
    {
        _input.LeftClicked.AddListener(_mover.MoveDown);
        _input.RightClicked.AddListener(_mover.MoveDown);

        _input.LeftClicked.AddListener(CheckPosition);
        _input.RightClicked.AddListener(CheckPosition);

        _branchAnim.AnimationEnded += OnAnimationEnd;
    }

    //if y coord of branch equal 7.5 its mean that the branch on player level coordinate
    private void CheckPosition()
    {
        if (_transform.localPosition.y == 7.5f)//and now we need to check side of player, in GameController
        {
            if (!Player.Instance.CheckCollisionWithBranch(this))//branch was cut
            {
                //play anim
                _mover.enabled = false;
                _branchAnim.PlayCutAnimation(BranchSide);
            }
        }
    }

    private void OnAnimationEnd()
    {
        BranchCut?.Invoke(this);        
        _mover.enabled = true;
    }

    private void OnDisable()
    {
        _input.LeftClicked.RemoveListener(_mover.MoveDown);
        _input.RightClicked.RemoveListener(_mover.MoveDown);

        _branchAnim.AnimationEnded -= OnAnimationEnd;

    }

    private void Start()
    {
        _transform.localPosition = new Vector2(_branchSide.SideCoordinate, _transform.localPosition.y);
        _branchSide.SetRandomSide();
    }

    public void SetPositionY(float y_coord)
    {
        _transform.localPosition = new Vector2(_branchSide.SideCoordinate, y_coord);
        _transform.localRotation = Quaternion.identity;
    }

    public void SetRandomSide()
    {
        _branchSide.SetRandomSide();
    }

}
