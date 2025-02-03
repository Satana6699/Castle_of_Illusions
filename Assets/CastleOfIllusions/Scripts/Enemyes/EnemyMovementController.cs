using UnityEngine;
using UnityEngine.Serialization;

[RequireComponent(typeof(Rigidbody))]
public class EnemyMovementController : MonoBehaviour
{
    [Header("Movement Boundaries")]
    [SerializeField] private float leftPredelPositionForX;
    [SerializeField] private float righrPredelPositionForX;

    [Header("Movement Settings")]
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private bool isMoving = true;

    private bool _facingRight = true;
    private float _moveInput = 1;

    private Rigidbody _rigidbody;

    void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        if (_moveInput == -1 && transform.position.x <= leftPredelPositionForX)
        {
            _moveInput = 1;
        }
        else if (_moveInput == 1 && transform.position.x >= righrPredelPositionForX)
        {
            _moveInput = -1;
        }
        
        if (isMoving)
        {
            Vector3 newPosition = _rigidbody.position + Vector3.right * _moveInput * moveSpeed * Time.fixedDeltaTime;
            _rigidbody.MovePosition(newPosition);
        }
        
        Rotate();
    }
    
    private void Rotate()
    {
        if (_moveInput > 0 && !_facingRight)
        {
            transform.rotation = Quaternion.Euler(0, -90, 0);
            _facingRight = true;
        }
        else if (_moveInput < 0 && _facingRight)
        {
            transform.rotation = Quaternion.Euler(0, 90, 0);
            _facingRight = false;
        }
    }
}
