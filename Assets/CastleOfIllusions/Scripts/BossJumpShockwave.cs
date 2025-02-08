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
        if (gameSettings)
        {
            _damage = gameSettings.bossDamage;
        }
        
        if (boomParticles)
        {
            Instantiate(boomParticles, transform.position, Quaternion.identity);

            Invoke(nameof(Death), boomParticles.main.duration);
        }
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out PlayerHealth playerHealth))
        {
            playerHealth.TakeDamage(_damage);
        }
    }

    private void Death()
    {
        Destroy(gameObject);
    }
}
