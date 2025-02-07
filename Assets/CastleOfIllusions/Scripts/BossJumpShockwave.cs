using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossJumpShockwave : MonoBehaviour
{
    [SerializeField] private GameSettings gameSettings;
    
    private float _damage = 5f;
    [SerializeField] private ParticleSystem boomParticles;

    private void Start()
    {
        if (gameSettings is not null)
        {
            _damage = gameSettings.bossDamage;
        }
        
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
                playerHealth.TakeDamage(_damage);
            }
        }
    }

    private void Death()
    {
        Destroy(gameObject);
    }
}
