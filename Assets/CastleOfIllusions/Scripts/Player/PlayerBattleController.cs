using UnityEngine;
using UnityEngine.Serialization;

public class PlayerBattleController : MonoBehaviour
{
    [SerializeField] private GameSettings gameSettings;
    
    [Header("Attack Settings")]
    private float _speedAttack = 1f;
    private float _timerAttack = 0f;
    private float _damage = 10f;
    private bool _isAttacking = false;
    
    [Header("Sword Settings")]
    [SerializeField] private Animator animator;
    private BoxCollider _registerDamageZone;


    void Start()
    {
        if (gameSettings)
        {
            _damage = gameSettings.playerDamage;
            _speedAttack = gameSettings.playerSpeedAttack;
        }
        
        _registerDamageZone = GetComponent<BoxCollider>();
        _registerDamageZone.enabled = false;
        
        animator ??= GetComponent<Animator>();
        _timerAttack = _speedAttack;
    }
    
    void Update()
    {
        _timerAttack += Time.deltaTime;
        
        if (Input.GetKeyDown(KeyCode.Mouse0) && _timerAttack >= _speedAttack)
        {
            Attack();
            _timerAttack = 0f;
        }
    }

    public void RegisterDamageEnable()
    {
        _registerDamageZone.enabled = true;
        _isAttacking = true;
    }
    
    public void RegisterDamageDisable()
    {
        Invoke(nameof(RegisterDamageDisableWaitSeconds), 0.3f);
    }

    private void RegisterDamageDisableWaitSeconds()
    {
        _registerDamageZone.enabled = false;
        _isAttacking = false;
    }
    
    private void Attack()
    {
        animator.SetTrigger("Attack");
        AudioManager.Instance?.PlaySFX(AudioManager.Instance?.soundSettings.swordAttackSound);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out EnemyHealth enemyHealth) && _isAttacking)
        {
            enemyHealth.TakeDamage(_damage);
            AudioManager.Instance?.PlaySFX(AudioManager.Instance?.soundSettings.swordHitSound);
        }
    }
}
