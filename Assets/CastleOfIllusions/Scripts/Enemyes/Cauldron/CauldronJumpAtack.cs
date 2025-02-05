using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

[RequireComponent(typeof(Rigidbody))]
public class CauldronJumpAtack : MonoBehaviour
{
    [SerializeField] private float jumpForceX = 5f;
    [SerializeField] private float jumpForceY = 5f;
    [SerializeField] private float jumpCalldown = 5f;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private float radiusGround = 0.1f;
    [SerializeField] private GameObject player;
    
    [SerializeField] private GameObject boomGroundPrefab;
    
    private Animator _animatorBoiler;
    private Rigidbody _rigidbody;
    private bool _isGrounded = false;
    private bool _wasGrounded = false;
    
    void Start()
    {
        _animatorBoiler = gameObject.GetComponent<Animator>();
        _rigidbody = GetComponent<Rigidbody>();
        
        StartCoroutine(Jump());
    }

    void Update()
    {
        CheckGround();

        if (!_wasGrounded && _isGrounded)
        {
            Instantiate(boomGroundPrefab, groundCheck.position, Quaternion.identity);
        }

        _animatorBoiler.SetBool("IsGrounded", _isGrounded);
        
        _wasGrounded = _isGrounded;
    }

    private IEnumerator Jump()
    {
        Vector3 jumpVector = player.transform.position - transform.position;
        jumpVector = new Vector3(jumpVector.x, 0, 0).normalized;
        _rigidbody.AddForce((Vector3.up * jumpForceY + jumpVector * jumpForceX), ForceMode.Impulse);
        yield return new WaitForSeconds(jumpCalldown);
        
        StartCoroutine(Jump());
    }
    private void CheckGround() => _isGrounded = Physics.CheckSphere(groundCheck.position, radiusGround, groundLayer);
}
