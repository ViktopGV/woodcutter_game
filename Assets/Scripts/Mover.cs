using UnityEngine;

public class Mover : MonoBehaviour
{
    [SerializeField] private float stepSize = 5.5f;
    
    private Transform _transform;

    private void Start()
    {
        _transform = transform;
    }

    public void MoveDown()
    {
        _transform.Translate(Vector2.down * stepSize);        
    }

}
