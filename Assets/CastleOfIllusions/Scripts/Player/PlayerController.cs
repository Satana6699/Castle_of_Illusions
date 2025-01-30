using System;
using UnityEngine;

namespace CastleOfIllusions.Scripts
{
    [RequireComponent(typeof(PlayerHealth))]
    public class PlayerController : MonoBehaviour
    {
        [Header("Move Stats")]
        [SerializeField] private float moveSpeed = 10f; 
        [SerializeField] private float acceleration = 5f;
        private float _moveInput;
    
        public bool FacingRight { get; private set; } = true;
    
        [Header("Jump Stats")]
        [SerializeField] private float jumpForce = 100f;
        private bool _isGrounded = false;

        [Header("Настройки земли")]
        [SerializeField] private Transform groundCheck;
        [SerializeField] private LayerMask groundLayer;
        [SerializeField] private float radiusGround = 0.1f;
    
        private Animator _animator;
        private Rigidbody _rigidbody;
        private PlayerHealth _playerHealth;
    
        private void Start()
        {
            _rigidbody = GetComponent<Rigidbody>();
            _animator = GetComponent<Animator>();
            _playerHealth = GetComponent<PlayerHealth>();
            _rigidbody.constraints = RigidbodyConstraints.FreezeRotation;
        }

        private void FixedUpdate()
        {
            Move();

            Rotate();
        }

        private void Move()
        {
            CheckGround();
        
            _moveInput = Input.GetAxis("Horizontal");

            _rigidbody.AddForce(new Vector3(_moveInput * acceleration, 0f, 0f), ForceMode.Acceleration);
        
            Vector3 velocity = _rigidbody.velocity;
            velocity.x = Mathf.Clamp(velocity.x, -moveSpeed, moveSpeed);
            _rigidbody.velocity = velocity;
        }

        private void Update()
        {
            _animator.SetBool("Grounded", _isGrounded);
        
            float normalisedAcceleration = Mathf.Clamp(_moveInput * acceleration / moveSpeed, -1f, 1f);
            _animator.SetFloat("MoveSpeed", Math.Abs(normalisedAcceleration));
        
            Jump();

            FreezMoveForZ();
        }

        private void Jump()
        {
            if (Input.GetKeyDown(KeyCode.Space) && _isGrounded)
            {
                _rigidbody.velocity = new Vector3(_rigidbody.velocity.x, jumpForce, _rigidbody.velocity.z);
            }
        }

        private void FreezMoveForZ() => transform.position = new Vector3(transform.position.x, transform.position.y, 0);

        private void Rotate()
        {
            if (_moveInput > 0 && !FacingRight)
            {
                transform.rotation = Quaternion.Euler(0, 90, 0);
                FacingRight = true;
            }
            else if (_moveInput < 0 && FacingRight)
            {
                transform.rotation = Quaternion.Euler(0, -90, 0);
                FacingRight = false;
            }
        }

        private void CheckGround() => _isGrounded = Physics.CheckSphere(groundCheck.position, radiusGround, groundLayer);
    }
}