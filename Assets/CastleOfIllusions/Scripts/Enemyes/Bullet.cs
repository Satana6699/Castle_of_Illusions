using CastleOfIllusions.Scripts;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [Header("Bullet Settings")]
    [SerializeField] private float damage = 10f;
    [SerializeField] private float speed = 1f;
    [SerializeField] private float timeLive = 2f;

    [Header("Particle Settings")]
    [SerializeField] private new ParticleSystem particleSystem = null;

    private float _timerLive = 0f;

    private void Update()
    {
        _timerLive += Time.deltaTime;
        
        if (_timerLive >= timeLive)
        {
            Death();
        }
        
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }

    private void Death()
    {
        Instantiate(particleSystem, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            var playerHealth = other.gameObject.GetComponent<PlayerHealth>();
            playerHealth?.TakeDamage(damage);
        }
        
        Death();
    }
}