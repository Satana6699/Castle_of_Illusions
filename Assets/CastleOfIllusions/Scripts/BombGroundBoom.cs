using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombGrundBoom : MonoBehaviour
{
    [SerializeField] private float damage = 5f;
    [SerializeField] private ParticleSystem boomParticles;

    private void Start()
    {
        if (boomParticles is not null)
        {
            Instantiate(boomParticles, transform.position, Quaternion.identity);

            Invoke(nameof(Death), boomParticles.main.duration);
        }
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            var playerHealth = other.gameObject.GetComponent<PlayerHealth>();
            
            if (playerHealth is not null)
            {
                playerHealth.TakeDamage(damage);
            }
        }
    }

    private void Death()
    {
        Destroy(gameObject);
    }
}
