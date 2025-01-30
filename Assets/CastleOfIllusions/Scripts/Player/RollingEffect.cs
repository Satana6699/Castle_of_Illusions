using System.Collections;
using UnityEngine;

namespace CastleOfIllusions.Scripts
{
    [RequireComponent(typeof(PlayerController))]
    [RequireComponent(typeof(CapsuleCollider))]
    public class RollingEffect : MonoBehaviour
    {
        [Header("Rolling Stats")]
        [SerializeField] private float rollSpeed = 5f;
        [SerializeField] private float rollDuration = 1f; 
        [SerializeField] private float rollCooldown = 1f; 
    
        [Header("Layers Settings")]
        [SerializeField] private LayerMask playerLayer;
        [SerializeField] private LayerMask enemyLayer;
    
        private Rigidbody _rigidbody = null;
        private Animator _animator = null;
        private PlayerController _playerController = null;
        private CapsuleCollider _collider = null;
        private bool _isRolling = false;
        private float _rollTime = 0f;
        private float _baseHeightCollider;
        private Vector3 _baseCenterCollider;

        void Start()
        {
            _rigidbody = GetComponent<Rigidbody>();
            _animator = GetComponent<Animator>();
            _playerController = GetComponent<PlayerController>();
            _playerController = GetComponent<PlayerController>();
            _collider = GetComponent<CapsuleCollider>();
            _baseHeightCollider = _collider.height;
            _baseCenterCollider = _collider.center;
        }

        void Update()
        {
            _rollTime += Time.deltaTime;
        
            if (_rollTime >= rollCooldown && Input.GetKeyDown(KeyCode.LeftShift) && !_isRolling)
            {
                StartCoroutine(Roll());
                _rollTime = 0f;
            }
        }

        void FixedUpdate()
        {
            float impulse = 0.5f;
            if (_isRolling)
            {
                Vector3 targetPosition = _rigidbody.position + transform.forward * impulse * rollSpeed * Time.fixedDeltaTime;
                targetPosition.y = _rigidbody.position.y;
                _rigidbody.MovePosition(targetPosition);
            }
        }
    
        private IEnumerator Roll()
        {
            yield return new WaitForSeconds(0.01f);
            SetRollState(true);
            yield return new WaitForSeconds(rollDuration);
            SetRollState(false);
            yield return new WaitForSeconds(0.01f);
        }

        public bool CheckRoll() => _isRolling;

        private void SetRollState(bool isRolling)
        {
            _isRolling = isRolling;
            _playerController.enabled = !isRolling;
            RollAnimation(isRolling);
            Physics.IgnoreLayerCollision(playerLayer, enemyLayer, isRolling);
            if (isRolling)
            {
                _collider.height = 0.2f;
                _collider.center = new Vector3(_collider.center.x, 0.1f, _collider.center.z);
            }
            else
            {
                _collider.height = _baseHeightCollider;
                _collider.center = _baseCenterCollider;
            }
            
        }
    
        private void RollAnimation(bool isRolled)
        {
            //_animator.SetBool("Roll", isRolled);
        }
    }
}
