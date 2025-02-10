using System;
using UnityEngine;
using UnityEngine.Serialization;

[RequireComponent(typeof(CharacterController))]
public class ChestController : MonoBehaviour
    {
        [SerializeField] private GameSettings gameSettings;
        
        private float _moveSpeed = 10f;
        private float _gravityForce = -9.81f;
        
        [SerializeField] private bool isMoving = true;
        [SerializeField] private Animator animator;
        [SerializeField] private GameObject leftLimit,  rightLimit;
        
        private CharacterController _controller;
        private Vector3 _velocity;
        private float _moveX = 1;
        private Vector3 _leftLimitPosition, _rightLimitPosition;
        private Quaternion _moveRotation;

        private void Start()
        {
            _leftLimitPosition = leftLimit.transform.position;
            _rightLimitPosition = rightLimit.transform.position;
            
            Destroy(leftLimit);
            Destroy(rightLimit);
            
            if (gameSettings)
            {
                _moveSpeed = gameSettings.chestSpeed;
                _gravityForce = gameSettings.chestGravityForce;
            }
            
            if (!animator)
            {
                animator = GetComponent<Animator>();
            }
        
            if (animator)
            {
                animator.SetBool("IsMoving", isMoving);
            }
            
            _controller = GetComponent<CharacterController>();

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
            float yRotation = transform.eulerAngles.y;
            float yRotationError = 5f;
            
            if (_moveX > 0 && (yRotation < -90 + yRotationError && yRotation > -90 - yRotationError || 
                               yRotation < 270 + yRotationError && yRotation > 270 - yRotationError))
            {
                transform.rotation = Quaternion.Euler(0, 90, 0);
            }
            else if (_moveX < 0 && (yRotation < 90 + yRotationError && yRotation > 90 - yRotationError || 
                                    yRotation < -270 + yRotationError && yRotation > -270 - yRotationError))
            {
                transform.rotation = Quaternion.Euler(0, -90, 0);
            }
        }
    }