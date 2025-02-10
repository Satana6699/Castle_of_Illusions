using System;
using UnityEngine;
using UnityEngine.Serialization;

[RequireComponent(typeof(CharacterController))]
public class ChairController : MonoBehaviour
    {
        [SerializeField] private GameSettings gameSettings;
        
        private float _moveSpeed = 10f;
        private float _gravityForce = -9.81f;
        
        [SerializeField] private GameObject leftLimitPosition;
        [SerializeField] private GameObject rightLimitPosition;
        [SerializeField] private bool isMoving = true;
        [SerializeField] private Animator animator;
        
        private CharacterController _controller;
        private Vector3 _velocity;
        private float _moveX = 1;
        private bool _facingRight;
        private Vector3 _leftLimitPosition, _rightLimitPosition;
        private void Start()
        {
            if (gameSettings)
            {
                _moveSpeed = gameSettings.chairSpeed;
                _gravityForce = gameSettings.chairGravityForce;
            }
            
            animator = GetComponent<Animator>();
        
            if (animator)
            {
                animator.SetBool("IsMoving", isMoving);
            }
            
            _controller = GetComponent<CharacterController>();

            _leftLimitPosition = leftLimitPosition.transform.position;
            _rightLimitPosition = rightLimitPosition.transform.position;
            
            Destroy(leftLimitPosition);
            Destroy(rightLimitPosition);
        }

        private void Update()
        {
            if (Mathf.Approximately(_moveX, -1) && transform.position.x <= _leftLimitPosition.x)
            {
                _moveX = 1;
            }
            else if (Mathf.Approximately(_moveX, 1) && transform.position.x >= _rightLimitPosition.x)
            {
                _moveX = -1;
            }
            
            // Horizontal move
            Vector3 move = Vector3.right * _moveX * _moveSpeed;

            // Vertical move
            move.y = _velocity.y;

            
            if (isMoving)
            {
                _controller.Move(move * Time.deltaTime);
                Rotate();
            }
            
            // Gravity
            _velocity.y += _gravityForce * Time.deltaTime;
            
            // freeze z
            transform.position = new Vector3(transform.position.x, transform.position.y, 0);
        }

        public void StopMoving()
        {
            isMoving = false;
        }

        public void StartMoving()
        {
            isMoving = true;
            animator.SetBool("isAttacking", false);
        }
    
        private void Rotate()
        {
            if (_moveX > 0 && !_facingRight)
            {
                transform.rotation = Quaternion.Euler(0, -90, 0);
                _facingRight = true;
            }
            else if (_moveX < 0 && _facingRight)
            {
                transform.rotation = Quaternion.Euler(0, 90, 0);
                _facingRight = false;
            }
        }
    }