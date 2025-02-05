using System;
using UnityEngine;
using UnityEngine.Serialization;

public class PlayerController : MonoBehaviour
    {
        [SerializeField] private float moveSpeed = 10f;
        [SerializeField] private float jumpForce = 12f;
        [SerializeField] private float gravityForce = -9.81f;
        [SerializeField] private Animator animator;
        
        private CharacterController _controller;
        private Vector3 _velocity;
        private bool _isGrounded;
        private float _moveX;
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

            if (_velocity.y > 0 && (_controller.collisionFlags & CollisionFlags.Above) != 0)
            {
                _velocity.y = 0;
            }

            Animation();
            
            // Use gravity
            _velocity.y += gravityForce * Time.deltaTime;
            
            // freeze z
            transform.position = new Vector3(transform.position.x, transform.position.y, 0);
        }

        private void Move()
        {
            Vector3 move = Vector3.right * _moveX * moveSpeed;

            move.y = _velocity.y;

            _controller.Move(move * Time.deltaTime);
        }

        private void Jump()
        {
            if (_isGrounded && Input.GetButtonDown("Jump"))
            {
                _velocity.y = Mathf.Sqrt(jumpForce * -2f * gravityForce);
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