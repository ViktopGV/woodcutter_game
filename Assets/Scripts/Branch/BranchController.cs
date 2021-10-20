using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BranchController : MonoBehaviour
{
    [SerializeField] private Branch _branchTemplate;
    [SerializeField] private int _branchCount = 5;

    private List<Branch> _branches;

    private const float START_BRANCH_Y_POS = 13;
    private const float OFFSET_BETWEEN_BRANCHES = 5.5f;


    private void Awake()
    {
        _branches = new List<Branch>();
        InstantiateBranches();
    }

    private void OnEnable()
    {
        foreach (Branch branch in _branches)
            branch.BranchCut += RespawnBranch;
    }

    private void OnDisable()
    {
        foreach (Branch branch in _branches)
            branch.BranchCut -= RespawnBranch;
    }

    private void InstantiateBranches()
    {
        for (int i = 0; i < _branchCount; ++i)
        {
            _branches.Add(Instantiate(_branchTemplate, transform));
            float y_pos = i == 0 ? START_BRANCH_Y_POS : GetPreviousBranch(_branches[i]).transform.localPosition.y + OFFSET_BETWEEN_BRANCHES;
            _branches[i].SetPositionY(y_pos);
            _branches[i].name = "branch" + i;
            _branches[i].BranchCut += RespawnBranch;
        }
    }

    public void RespawnBranch(Branch branch)
    {        
        StartCoroutine(WaitBranchUp(branch));
    }

    private IEnumerator WaitBranchUp(Branch branch)
    {
        float y_coord = 0;
        do
        {
            y_coord = GetPreviousBranch(branch).transform.localPosition.y + OFFSET_BETWEEN_BRANCHES;
            yield return null;
        } while (y_coord < 13);

        branch.SetPositionY(y_coord);
        branch.SetRandomSide();
        branch.Anim.SetSpriteAlpha(1);
    }

    private Branch GetPreviousBranch(Branch branch)
    {
        int currentBranchIndex = _branches.IndexOf(branch);
        if (currentBranchIndex <= 0) return _branches[_branches.Count - 1];
        return _branches[currentBranchIndex - 1];
    }
}
