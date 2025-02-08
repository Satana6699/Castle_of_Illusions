using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour
{
    [Header("Health Stats")]
    protected float Health = 100f;
    [SerializeField] protected ParticleSystem damageParticles;
    [SerializeField] protected Image healthBar;

    private float _maxHealth;
    private ChanceDropHealth _chanceDropHealth;
    
    protected virtual void Start()
    {
        _maxHealth = Health;
        _chanceDropHealth = GetComponent<ChanceDropHealth>();
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
        Health -= damage;
        
        if (damageParticles)
        {
            Instantiate(damageParticles, transform.position, Quaternion.identity);
        }
            
        if (Health <= 0)
        {
            Health = 0;
            Death();
        }

        UpdateHealthBar();
    }
    
    private void Death()
    {
        _chanceDropHealth?.SpawnHealInChance();
        Destroy(gameObject);
    }

    private void UpdateHealthBar()
    {
        if (healthBar)
        {
            healthBar.fillAmount = Health / _maxHealth;
        }
    }
}
