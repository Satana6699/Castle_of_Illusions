using System.Collections;
using UnityEngine;

[RequireComponent(typeof(CauldronDistanceAtack))]
[RequireComponent(typeof(CauldronJumpAtack))]
public class CauldronController : MonoBehaviour
{
    [SerializeField] private GameSettings gameSettings;
    
    private float _distanceForDistanceAttack = 3f;
    private float _attackCooldown = 3f;
    private float _detectionRadius = 5f;

    private CauldronJumpAtack _jumpAttack;
    private CauldronDistanceAtack _attack;
    private float _timerAttackCooldown = 0f;

    private Transform _player;

    private void Start()
    {
        if (gameSettings)
        {
            _distanceForDistanceAttack = gameSettings.distanceForDistanceAttack;
            _attackCooldown = gameSettings.bossAttackCooldown;
            _detectionRadius = gameSettings.bossDetectionRadius;
        }

        _jumpAttack = GetComponent<CauldronJumpAtack>();
        _attack = GetComponent<CauldronDistanceAtack>();
        _timerAttackCooldown = _attackCooldown;
    }

    private void Update()
    {
        _timerAttackCooldown += Time.deltaTime;
        CheckForPlayer();
    }

    private void CheckForPlayer()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, _detectionRadius);
        
        foreach (var hitCollider in hitColliders)
        {
            if (hitCollider.CompareTag("Player"))
            {
                _player = hitCollider.transform;
                StartCoroutine(Attack(_player));
                break;
            }
        }
    }

    private IEnumerator Attack(Transform playerTransform)
    {
        yield return new WaitForSeconds(0.1f);

        if (!playerTransform) yield break;

        float distance = Vector3.Distance(transform.position, playerTransform.position);

        if (_timerAttackCooldown >= _attackCooldown)
        {
            if (distance >= _distanceForDistanceAttack)
            {
                StartCoroutine(_attack.SpawnBomb());
            }
            else
            {
                StartCoroutine(_jumpAttack.Jump());
            }

            _timerAttackCooldown = 0;
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, _detectionRadius);
    }
}
