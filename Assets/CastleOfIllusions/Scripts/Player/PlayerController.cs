using System;
using UnityEngine;
using UnityEngine.Serialization;

public class PlayerController : MonoBehaviour
    {
        [SerializeField] private GameSettings gameSettings;
        
        [Header("Player settings")]
        private float _moveSpeed = 6f;
        private float _jumpForce = 4f;
        private float _gravityForce = -40f;
        [SerializeField] private Animator animator = null;
        
        private CharacterController _controller;
        private Vector3 _velocity = Vector3.zero;
        private bool _isGrounded = false;
        private float _moveX = 0;
        private bool _facingRight  = true;
        
        private void Start()
        {
            if (gameSettings)
            {
                _moveSpeed = gameSettings.playerSpeed;
                _jumpForce = gameSettings.playerJumpForce;
                _gravityForce = gameSettings.playerGravityForce;
            }
            
            _controller = GetComponent<CharacterController>();
            animator ??= GetComponent<Animator>();
        }

        private void Update()
        {
            _isGrounded = _controller.isGrounded;

            if (_isGrounded && _velocity.y < 0)
            {
                _velocity.y = -2f;
            }

            _moveX = Input.GetAxis("Horizontal");
            
            if (_moveX != 0 && _isGrounded)
            {
                AudioManager.Instance?.PlaySFXNoRepeat(AudioManager.Instance?.soundSettings.playerMovementSound);
            }
            
            Move();
            Rotate();
            Jump();

            // Check head collision
            if (_velocity.y > 0 && (_controller.collisionFlags & CollisionFlags.Above) != 0)
            {
                _velocity.y = 0;
            }

            Animation();
            
            // Use gravity
            _velocity.y += _gravityForce * Time.deltaTime;
            
            // freeze z
            transform.position = new Vector3(transform.position.x, transform.position.y, 0);
        }

        private void Move()
        {
            Vector3 move = Vector3.right * _moveX * _moveSpeed;

            move.y = _velocity.y;
            
            _controller.Move(move * Time.deltaTime);
        }

        private void Jump()
        {
            if (_isGrounded && Input.GetButtonDown("Jump"))
            {
                _velocity.y = Mathf.Sqrt(_jumpForce * -2f * _gravityForce);
                AudioManager.Instance?.PlaySFXNoRepeat(AudioManager.Instance?.soundSettings.playerJumpSound);
            }
        }
        
        private void Rotate()
        {
            if (_moveX > 0 && !_facingRight)
            {
                transform.rotation = Quaternion.Euler(0, 90, 0);
                _facingRight = true;
            }
            else if (_moveX < 0 && _facingRight)
            {
                transform.rotation = Quaternion.Euler(0, -90, 0);
                _facingRight = false;
            }
        }
        
        private void Animation()
        {
            animator.SetBool("Grounded", _isGrounded);
        
            float normalisedAcceleration = _moveX;
            animator.SetFloat("MoveSpeed", Math.Abs(normalisedAcceleration));
        }
    }