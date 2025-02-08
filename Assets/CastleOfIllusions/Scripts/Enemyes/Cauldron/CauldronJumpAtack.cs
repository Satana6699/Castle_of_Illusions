using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

[RequireComponent(typeof(CharacterController))]
public class CauldronJumpAtack : MonoBehaviour
{
    [SerializeField] private GameSettings gameSettings;
    
    private float _jumpForce = 4f;
    private float _gravityForce = -40f;
    
    [SerializeField] private GameObject boomGroundPrefab;
    
    private CharacterController _characterController;
    private Vector3 _velocity;
    private bool _wasGrounded = false;
    
    void Start()
    {
        if (gameSettings)
        {
            _jumpForce = gameSettings.bossJumpForce;
            _gravityForce = gameSettings.bossGravityForce;
        }
        
        _characterController = GetComponent<CharacterController>();
    }

    void Update()
    {
        if (_characterController.isGrounded && _velocity.y < 0)
        {
            _velocity.y = -2f;
        }
        
        // Use gravity
        _velocity.y += _gravityForce * Time.deltaTime;
        
        JumpMove();
            
        // freeze z
        transform.position = new Vector3(transform.position.x, transform.position.y, 0);
        
        Приземлился();

        _wasGrounded = _characterController.isGrounded;
    }

    public IEnumerator Jump()
    {
        yield return new WaitForSeconds(0.1f);
        
        if (_characterController.isGrounded)
        {
            _velocity.y = Mathf.Sqrt(_jumpForce * -2f * _gravityForce);
        }
    }
    
    private void JumpMove()
    {
        Vector3 move = Vector3.zero;

        move.y = _velocity.y;

        _characterController.Move(move * Time.deltaTime);
    }
    
    private void Приземлился()
    {
        if (!_wasGrounded && _characterController.isGrounded)
        {
            Instantiate(boomGroundPrefab, transform.position, Quaternion.identity);
        }
    }
}
