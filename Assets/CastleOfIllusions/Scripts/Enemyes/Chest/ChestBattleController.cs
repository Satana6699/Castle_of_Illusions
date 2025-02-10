using UnityEngine;
    
public class ChestBattleController : MonoBehaviour
{
    [SerializeField] private GameSettings gameSettings;
    
    private float _damage = 10f;
    [SerializeField] private Animator animator;

    private ChestController _movementController;
    private PlayerHealth _playerHealth;
    private Collider _otherCollider;
    
    private void Start()
    {
        if (gameSettings)
        {
            _damage = gameSettings.chestDamage;
        }
        
        _movementController = GetComponent<ChestController>();
        
        if (!animator)
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
            
            _otherCollider = other;
            
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
        AudioManager.Instance?.PlaySFX(AudioManager.Instance?.soundSettings.chestAttackSound);

        if (_otherCollider || _otherCollider.enabled)
        {
            _playerHealth?.TakeDamage(_damage);
        }
        else
        {
            _playerHealth = null;
            _movementController?.StartMoving();
        }
    }
}