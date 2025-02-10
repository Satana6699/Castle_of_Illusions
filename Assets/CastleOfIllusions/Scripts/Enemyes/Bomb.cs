using System.Collections;
using UnityEngine;

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
    private Vector3 _targetPosition;
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
        StartCoroutine(MoveProjectile());
    }

    public void Initialize(Transform targetPosition, float height)
    {
        _targetPosition = targetPosition.position;
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

    private IEnumerator MoveProjectile()
    {
        float duration = Vector3.Distance(_startPoint, _targetPosition) / _speed; // Время полета (постоянная скорость)
        float elapsed = 0f;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float t = elapsed / duration;

            // Линейное движение по XZ
            Vector3 pos = Vector3.Lerp(_startPoint, _targetPosition, t);

            // Добавление параболы по Y
            pos.y += _arcHeight * Mathf.Sin(t * Mathf.PI);

            transform.position = pos;
            yield return null;
        }

        Explode();
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
