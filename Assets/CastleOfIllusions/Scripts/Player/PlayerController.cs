using System;
using UnityEngine;
using UnityEngine.Serialization;

public class PlayerController : MonoBehaviour
    {
        [SerializeField] private float moveSpeed = 6f;
        [SerializeField] private float jumpForce = 4f;
        [SerializeField] private float airResistance = -40f;
        [SerializeField] private float gravityForce = -40f;
        [SerializeField] private Animator animator = null;
        
        private CharacterController _controller;
        private Vector3 _velocity = Vector3.zero;
        private bool _isGrounded = false;
        private float _moveX = 0;
        private bool _facingRight  = true;
        
        private void Start()
        {
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
            _velocity.y += gravityForce * Time.deltaTime;
            
            // Use air resistance
            _velocity.x += airResistance * Time.deltaTime;

            if (_velocity.x <= 0)
            {
                _velocity.x = 0;
            }
            
            // freeze z
            transform.position = new Vector3(transform.position.x, transform.position.y, 0);
        }

        private void Move()
        {
            Vector3 move = Vector3.right * _moveX * moveSpeed;

            move.y = _velocity.y;

            if (_velocity.x != 0)
            {
                move.x = _velocity.x;
            }
            
            _controller.Move(move * Time.deltaTime);
        }

        private void Jump()
        {
            if (_isGrounded && Input.GetButtonDown("Jump"))
            {
                _velocity.y = Mathf.Sqrt(jumpForce * -2f * gravityForce);
            }
        }

        public void AddForce(Vector3 force)
        {
            _velocity += force;
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