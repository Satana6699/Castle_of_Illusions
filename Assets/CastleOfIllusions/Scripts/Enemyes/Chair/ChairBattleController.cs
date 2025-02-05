using UnityEngine;
    
public class ChairBattleController : MonoBehaviour
{
    [SerializeField] private float damage = 10f;
    [SerializeField] private Animator animator;

    private ChairController _movementController;
    private PlayerHealth _playerHealth;
    
    private void Start()
    {
        _movementController = GetComponent<ChairController>();
        
        if (animator is null)
        {
            animator = GetComponent<Animator>();
        }
    }
    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.gameObject.CompareTag("Player"))
        {
            _movementController?.StopMoving();
                
            Vector3 contactPoint = hit.point;
            Vector3 targetDirection = contactPoint - transform.position;
            Quaternion targetRotation = Quaternion.LookRotation(targetDirection);
            transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, targetRotation.eulerAngles.y, transform.rotation.eulerAngles.z);
            
            _playerHealth = hit.gameObject.GetComponent<PlayerHealth>();
                
            animator.SetBool("isAttacking", true);
        }
    }

    public void DamagePlayer()
    {
        Debug.Log("Player damaged");
        _playerHealth?.TakeDamage(damage);
    }
}