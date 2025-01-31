using System;
using UnityEngine;

namespace CastleOfIllusions.Scripts
{
    [RequireComponent(typeof(PlayerHealth))]
    public class PlayerController : MonoBehaviour
    {
        [Header("Move Stats")]
        [SerializeField] private float moveSpeed = 10f; 
        private float _moveInput;
    
        public bool FacingRight { get; private set; } = true;
    
        [Header("Jump Stats")]
        [SerializeField] private float jumpForce = 100f;
        private bool _isGrounded = false;

        [Header("Настройки земли")]
        [SerializeField] private Transform groundCheck;
        [SerializeField] private LayerMask groundLayer;
        [SerializeField] private float radiusGround = 0.1f;
    
        [Header("Layers Settings")]
        [SerializeField] private LayerMask playerLayer;
        [SerializeField] private LayerMask enemyLayer;
        
        private Animator _animator;
        private Rigidbody _rigidbody;
        private PlayerHealth _playerHealth;
    
        private void Start()
        {
            _rigidbody = GetComponent<Rigidbody>();
            _animator = GetComponent<Animator>();
            _playerHealth = GetComponent<PlayerHealth>();
            _rigidbody.constraints = RigidbodyConstraints.FreezeRotation;
            
            int playerLayerIndex = Mathf.RoundToInt(Mathf.Log(playerLayer.value, 2));
            int enemyLayerIndex = Mathf.RoundToInt(Mathf.Log(enemyLayer.value, 2));
            Physics.IgnoreLayerCollision(playerLayerIndex, enemyLayerIndex, true);
        }
        
        private void Update()
        {
            CheckGround();

            _animator.SetBool("Grounded", _isGrounded);
        
            float normalisedAcceleration = _moveInput;
            _animator.SetFloat("MoveSpeed", Math.Abs(normalisedAcceleration));
        
            Jump();
            Rotate();
        }
        
        private void FixedUpdate()
        {
            Move();
        }

        private void Move()
        {
            _moveInput = Input.GetAxis("Horizontal");
            Vector3 movePosition = transform.forward * Math.Abs(_moveInput) * moveSpeed * Time.fixedDeltaTime;
            //transform.position += movePosition;
            Vector3 newPosition = transform.position + movePosition;
            _rigidbody.MovePosition(newPosition);
        }

        private void Jump()
        {
            if (Input.GetKeyDown(KeyCode.Space) && _isGrounded)
            {
                _rigidbody.velocity = new Vector3(_rigidbody.velocity.x, jumpForce, _rigidbody.velocity.z);
            }
        }

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