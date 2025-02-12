using System;
using UnityEngine;
    
public class ChairBattleController : MonoBehaviour
{
    [SerializeField] private GameSettings gameSettings;
    
    private float _damage = 10f;
    [SerializeField] private Animator animator;

    private ChairController _movementController;
    private PlayerHealth _playerHealth;
    
    private void Start()
    {
        if (gameSettings)
        {
            _damage = gameSettings.chairDamage;
        }
        
        _movementController = GetComponent<ChairController>();
        
        if (animator is null)
        {
            animator = GetComponent<Animator>();
        }
    }

    private void OnTriggerStay(Collider other)
    {

        if (other.gameObject.CompareTag("Player"))
        {
            _movementController?.StopMoving();
                
            Vector3 targetPos = other.gameObject.transform.position;
            Vector3 targetDirection = targetPos - transform.position;
            Quaternion targetRotation = Quaternion.LookRotation(targetDirection);
            transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, targetRotation.eulerAngles.y, transform.rotation.eulerAngles.z);
            
            _playerHealth = other.gameObject.GetComponent<PlayerHealth>();
                
            animator.SetBool("isAttacking", true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            _playerHealth = null;
            _movementController?.StartMoving();
        }
        
    }

    public void DamagePlayer()
    {
        AudioManager.Instance?.PlaySFX(AudioManager.Instance?.soundSettings.chairAttackSound, 
            transform.position);

        _playerHealth?.TakeDamage(_damage);
    }
}