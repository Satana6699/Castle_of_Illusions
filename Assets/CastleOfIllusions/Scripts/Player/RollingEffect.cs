using System.Collections;
using UnityEngine;

public class RollingEffect : MonoBehaviour
    {
        [Header("Rolling Settings")]
        [SerializeField] private float rollSpeed = 5f;
        [SerializeField] private float rollDuration = 1f;
        [SerializeField] private float rollCooldown = 1f;

        [Header("Character Components")]
        private Animator _animator = null;
        private PlayerController _playerController = null;

        [Header("Rolling State")]
        [SerializeField] private float rollHeightCollider = 0f;
        [SerializeField] private Vector3 rollCenterCollider = new Vector3(0f, 0.5f, 0f);
        [SerializeField] private string ignoreLayerCollisionPlayer = "Player";
        [SerializeField] private string ignoreLayerCollisionEnemy = "Enemy";
        private bool _isRolling = false;
        private float _rollTime = 0f;

        [Header("Collider Settings")]
        private CharacterController _characterController = null;
        private float _baseHeightCollider;
        private Vector3 _baseCenterCollider;

        void Start()
        {
            _animator = GetComponent<Animator>();
            _playerController = GetComponent<PlayerController>();
            _characterController = GetComponent<CharacterController>();
            _baseHeightCollider = _characterController.height;
            _baseCenterCollider = _characterController.center;
        }

        private void Update()
        {
            _rollTime += Time.deltaTime;
        
            if (_rollTime >= rollCooldown && Input.GetKeyDown(KeyCode.LeftShift) && !_isRolling && _characterController.isGrounded)
            {
                StartCoroutine(Roll());
                _rollTime = 0f;
            }
            
            if (_isRolling)
            {
                Vector3 rollPosition = transform.forward.normalized * rollSpeed * Time.deltaTime;
                _characterController.Move(rollPosition);
            }
            
            // freeze z
            transform.position = new Vector3(transform.position.x, transform.position.y, 0);
        }
    
        private IEnumerator Roll()
        {
            yield return new WaitForSeconds(0.01f);
            SetRollState(true);
            
            AudioManager.Instance?.PlaySFXNoRepeat(AudioManager.Instance?.soundSettings.playerRollSound);

            yield return new WaitForSeconds(rollDuration);
            SetRollState(false);
            yield return new WaitForSeconds(0.01f);
        }

        public bool CheckRoll() => _isRolling;

        private void SetRollState(bool isRolling)
        {
            _isRolling = isRolling;
            _playerController.enabled = !isRolling;
            
            Physics.IgnoreLayerCollision(LayerMask.NameToLayer(ignoreLayerCollisionPlayer),
                LayerMask.NameToLayer(ignoreLayerCollisionEnemy), isRolling);
            
            RollAnimation(isRolling);
            
            //RollCollider(isRolling);
            
        }

        private void RollCollider(bool isRolling)
        {
            if (isRolling)
            {
                _characterController.height = rollHeightCollider;
                _characterController.center = rollCenterCollider;
            }
            else
            {
                _characterController.height = _baseHeightCollider;
                _characterController.center = _baseCenterCollider;
            }
        }

        private void RollAnimation(bool isRolled)
        {
            _animator.SetBool("Roll", isRolled);
        }
    }