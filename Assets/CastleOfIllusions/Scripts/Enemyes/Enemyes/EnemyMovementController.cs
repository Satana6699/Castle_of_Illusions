using UnityEngine;
using UnityEngine.Serialization;

[RequireComponent(typeof(CharacterController))]
public class EnemyMovementController : MonoBehaviour
{
    [Header("Movement Boundaries")]
    [SerializeField] private float leftPredelPositionForX;
    [SerializeField] private float righrPredelPositionForX;

    [Header("Movement Settings")]
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private bool isMoving = true;
    [SerializeField] private Animator animator;

    private bool _facingRight = true;
    private float _moveInput = 1;

    private CharacterController _characterController;
    
    void Start()
    {
        animator = GetComponent<Animator>();
        
        if (animator is not null)
        {
            animator.SetBool("IsMoving", isMoving);
        }
        
        _characterController = GetComponent<CharacterController>();
    }

    void FixedUpdate()
    {
        if (Mathf.Approximately(_moveInput, -1) && transform.position.x <= leftPredelPositionForX)
        {
            _moveInput = 1;
        }
        else if (Mathf.Approximately(_moveInput, 1) && transform.position.x >= righrPredelPositionForX)
        {
            _moveInput = -1;
        }
        
        if (isMoving)
        {
            Vector3 newPosition = transform.position + Vector3.right * _moveInput * moveSpeed * Time.fixedDeltaTime;
            _characterController.Move(newPosition);
            Rotate();
        }
        
    }

    public void StopMoving()
    {
        isMoving = false;
    }

    public void StartMoving()
    {
        isMoving = true;
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
