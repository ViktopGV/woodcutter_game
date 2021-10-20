using UnityEngine;
using UnityEngine.Events;

public class Player : MonoBehaviour
{
    public static Player Instance { get; private set; }
    public UnityEvent PlayerCollided;
    public UnityEvent PlayerCutBranch;
    public UnityEvent PlayerRevivaled;

    [SerializeField] private Ad _ad;
    [SerializeField] private TextView _scoreText;
    [SerializeField] private TextView _bestScoreText;

    private SideHandler _sideHandler;
    private PlayerScore _playerScore;
    private Branch _killerBranch;

    public bool CheckCollisionWithBranch(Branch branch)
    {
        if (_sideHandler.Side == branch.BranchSide)
        {
            PlayerDied();
            SetGameOverText();
            _killerBranch = branch;
            return true;
        }
        PlayerCutBranch?.Invoke();
        return false;
    }

    public void PlayerDied()
    {
        PlayerCollided?.Invoke();
    }

    public void PlayerRevival()
    {
        PlayerRevivaled?.Invoke();
        AdRevival.SetScore(_playerScore.Score);
        //_killerBranch.transform.GetComponentInParent<BranchController>().RespawnBranch(_killerBranch);//spike
    }

    public void SetGameOverText()
    {
        _scoreText.SetText("Набрано " + _playerScore.Score);
        _bestScoreText.SetText("Лучший счет: " + _playerScore.BestScore);
    }


    private void OnEnable()
    {
        _ad.Rewarded += PlayerRevival;
        PlayerCutBranch.AddListener(_playerScore.IncreaseScore);
        _playerScore.ScoreIncreased += _scoreText.SetText;
    }

    private void OnDisable()
    {
        _ad.Rewarded -= PlayerRevival;
        PlayerCutBranch.RemoveListener(_playerScore.IncreaseScore);
        _playerScore.ScoreIncreased -= _scoreText.SetText;
    }

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);

        _sideHandler = GetComponent<SideHandler>();
        _playerScore = GetComponent<PlayerScore>();
    }

    private void Start()
    {
        _sideHandler.SetRandomSide();
        _scoreText.SetText(AdRevival.GetScore());
    }
}
