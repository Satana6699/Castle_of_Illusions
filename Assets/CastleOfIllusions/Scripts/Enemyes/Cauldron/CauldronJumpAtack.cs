using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

[RequireComponent(typeof(CharacterController))]
public class CauldronJumpAtack : MonoBehaviour
{
    [SerializeField] private GameSettings gameSettings;
    
    private float _moveSpeed = 6f;
    private float _jumpForce = 4f;
    private float _gravityForce = -40f;
    
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject boomGroundPrefab;
    
    private CharacterController _characterController;
    private Vector3 _velocity;
    private float _moveX;
    private bool _isGrounded = false;
    private bool _wasGrounded = false;
    
    private int _playerLayer = 0;
    private int _enemyLayer = 0;
    
    void Start()
    {
        if (gameSettings)
        {
            _moveSpeed = gameSettings.bossMoveSpeed;
            _jumpForce = gameSettings.bossJumpForce;
            _gravityForce = gameSettings.bossGravityForce;
        }
        
        _characterController = GetComponent<CharacterController>();
        _playerLayer = LayerMask.NameToLayer("Player");
        _enemyLayer = LayerMask.NameToLayer("Enemy");
    }

    void Update()
    {
        _isGrounded = _characterController.isGrounded;

        if (_isGrounded && _velocity.y < 0)
        {
            _velocity.y = -2f;
        }
        
        Move();
        
        // Use gravity
        _velocity.y += _gravityForce * Time.deltaTime;
            
        // freeze z
        transform.position = new Vector3(transform.position.x, transform.position.y, 0);
        
        Приземлился();

        _wasGrounded = _isGrounded;
    }

    public IEnumerator Jump()
    {
        yield return new WaitForSeconds(0.1f);
        
        //DisablePlayerBossCollision();
        
        if (_isGrounded)
        {
            Vector3 jumpVector = player.transform.position - transform.position;
            _moveX = new Vector3(jumpVector.x, 0, 0).normalized.x;
            _velocity.y = Mathf.Sqrt(_jumpForce * -2f * _gravityForce);
        }
    }
    
    private void Move()
    {
        Vector3 move = Vector3.right * _moveX * _moveSpeed;

        move.y = _velocity.y;

        _characterController.Move(move * Time.deltaTime);
    }
    
    private void Приземлился()
    {
        if (!_wasGrounded && _isGrounded)
        {
            _moveX = 0f;
            Instantiate(boomGroundPrefab, transform.position, Quaternion.identity);
            
        }
    }
    
    private void OnControllerColliderHit(ControllerColliderHit hit) 
    {
        if (hit.gameObject.CompareTag("Player"))
        {
            if (!_isGrounded)
            {
                StartCoroutine(AddForcePlayer(hit));
            }
        }
    }

    private IEnumerator AddForcePlayer(ControllerColliderHit hit)
    {
        yield return new WaitForSeconds(0.1f);
        
        var plaerController = hit.gameObject.GetComponent<PlayerController>();
        var forceVector = plaerController.transform.position - transform.position;
        forceVector = new Vector3(forceVector.x, 1, 0).normalized;
        DisablePlayerBossCollision();
        
        yield return new WaitForSeconds(0.1f);
        EnablePlayerBossCollision();
    }
    
    private void DisablePlayerBossCollision() 
    {
        Physics.IgnoreLayerCollision(_playerLayer, _enemyLayer, true);
    }

    private void EnablePlayerBossCollision() 
    {
        Physics.IgnoreLayerCollision(_playerLayer, _enemyLayer, false);
    }

}
