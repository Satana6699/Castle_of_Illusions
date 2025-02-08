using System.Collections;
using UnityEngine;
using UnityEngine.Serialization;

[RequireComponent(typeof(Rigidbody))]
public class Bomb : MonoBehaviour
{
    [SerializeField] private GameSettings gameSettings;
    
    [SerializeField] private ParticleSystem particleBoom;
    private float _explosionRadius = 2f;
    private float _damage = 20f;
    [SerializeField] private float timeNoBoom = 0.2f; 

    private float _speed = 10f;
    private float _arcHeight = 3f;
    private Vector3 _startPoint;
    private Transform _targetPosition;
    private float _timerTimeNoBoom = 0f;
    private bool _canBoom = false;

    void Start()
    {
        if (gameSettings)
        {
            _speed = gameSettings.speedDistanceBossAttack;
            _damage = gameSettings.bossDamage;
            _explosionRadius = gameSettings.bossExplosionRadius;
        }

        _startPoint = transform.position;
        if (_targetPosition)
        {
            StartCoroutine(MoveProjectile(_targetPosition.position));
        }
    }

    public void Initialize(Transform targetPosition, float height)
    {
        _targetPosition = targetPosition;
        _arcHeight = height;
    }

    private void Update()
    {
        _timerTimeNoBoom += Time.deltaTime;

        if (_timerTimeNoBoom >= timeNoBoom)
        {
            _canBoom = true;
        }
    }

    private IEnumerator MoveProjectile(Vector2 targetPos)
    {
        float duration = Vector2.Distance(_startPoint, targetPos) / _speed;
        for (float t = 0; t <= 1; t += Time.deltaTime / duration)
        {
            float x = Mathf.Lerp(_startPoint.x, targetPos.x, t);
            float y = Mathf.Lerp(_startPoint.y, targetPos.y, t) + _arcHeight * Mathf.Sin(t * Mathf.PI);
            transform.position = new Vector2(x, y);
            yield return null;
        }

        if (_canBoom)
        {
            Explode();
        }
    }

    void Explode()
    {
        Instantiate(particleBoom, transform.position, Quaternion.identity);
        CheckExplosionDamage();
        Destroy(gameObject);
    }

    void CheckExplosionDamage()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, _explosionRadius);
        foreach (Collider collider in hitColliders)
        {
            if (collider.CompareTag("Player"))
            {
                PlayerHealth playerHealth = collider.GetComponent<PlayerHealth>();
                if (playerHealth)
                {
                    playerHealth.TakeDamage(_damage);
                }
            }
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (_canBoom)
        {
            Explode();
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, _explosionRadius);
    }
}
