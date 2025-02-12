using UnityEngine;
using UnityEngine;
using UnityEngine.Serialization;
using System.Collections.Generic;

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
    
    [SerializeField] private Vector3 size = new Vector3(1f, 1f, 1f);
    [SerializeField] private string detectionLayer = "Enemy";
    [SerializeField] private Color gizmoColor = Color.red;
    [SerializeField] private float offsetForward = 1f;
    [SerializeField] private float offsetUp = 1f;
    
    private Vector3 _center;
    
    private HashSet<GameObject> _damagedEnemies = new HashSet<GameObject>();
    
    void Start()
    {
        if (gameSettings)
        {
            _damage = gameSettings.playerDamage;
            _speedAttack = gameSettings.playerSpeedAttack;
        }
        
        if (!animator)
        {
            animator = GetComponent<Animator>();
        }
        _timerAttack = _speedAttack;
        
        _center = transform.position + transform.forward * offsetForward;
        _center = _center + transform.up * offsetUp;
    }
    
    void Update()
    {
        _timerAttack += Time.deltaTime;
        
        _center = transform.position + transform.forward * offsetForward;
        _center = _center + transform.up * offsetUp;

        if (Input.GetKeyDown(KeyCode.Mouse0) && _timerAttack >= _speedAttack)
        {
            Attack();
            _timerAttack = 0f;
        }

        if (_isAttacking)
        {
            foreach (var other in CheckCollisions())
            {
                if (other.TryGetComponent(out EnemyHealth enemyHealth) && !_damagedEnemies.Contains(other.gameObject))
                {
                    enemyHealth.TakeDamage(_damage);
                    AudioManager.Instance?.PlaySFX(AudioManager.Instance?.soundSettings.swordHitSound, 
                        transform.position);
                    _damagedEnemies.Add(other.gameObject);
                }
            }
        }
    }

    private Collider[] CheckCollisions()
    {
        return Physics.OverlapBox(_center, size / 2, Quaternion.identity, LayerMask.GetMask(detectionLayer));
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = gizmoColor;
        Gizmos.DrawWireCube(_center, size);
    }
    
    public void RegisterDamageEnable()
    {
        _isAttacking = true;
        _damagedEnemies.Clear();
    }
    
    public void RegisterDamageDisable()
    {
        Invoke(nameof(RegisterDamageDisableWaitSeconds), 0.1f);
    }

    private void RegisterDamageDisableWaitSeconds()
    {
        _isAttacking = false;
    }
    
    private void Attack()
    {
        animator?.SetTrigger("Attack");
        AudioManager.Instance?.PlaySFX(AudioManager.Instance?.soundSettings.swordAttackSound, 
            transform.position);
    }
}
