using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour
{
    [Header("Health Stats")]
    [SerializeField] protected float health = 100f;
    [SerializeField] protected ParticleSystem damageParticles;
    [SerializeField] protected Image healthBar;

    private float _maxHealth;

    private void Start()
    {
        _maxHealth = health;
        UpdateHealthBar();
    }

    private void Update()
    {
        if (transform.rotation.y > 0)
        {
            healthBar.fillOrigin = 0;
        }
        else if (transform.rotation.y < 0)
        {
            healthBar.fillOrigin = 1;
        }
    }
    
    public void TakeDamage(float damage)
    {
        health -= damage;
        
        if (damageParticles is not null)
        {
            Instantiate(damageParticles, transform.position, Quaternion.identity);
        }
            
        if (health <= 0)
        {
            health = 0;
            Death();
        }

        UpdateHealthBar();
    }

    private void Death()
    {
        Destroy(gameObject);
    }

    private void UpdateHealthBar()
    {
        if (healthBar is not null)
        {
            healthBar.fillAmount = health / _maxHealth;
        }
    }
}
