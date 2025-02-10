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
        UpdateFillOrigin();
    }

    protected virtual void UpdateFillOrigin()
    {
        float yRotation = transform.eulerAngles.y;
        float yRotationError = 5f;
            
        if ((yRotation < -90 + yRotationError && yRotation > -90 - yRotationError || 
             yRotation < 270 + yRotationError && yRotation > 270 - yRotationError))
        {
            healthBar.fillOrigin = 1;
        }
        else if ((yRotation < 90 + yRotationError && yRotation > 90 - yRotationError || 
                  yRotation < -270 + yRotationError && yRotation > -270 - yRotationError))
        {
            healthBar.fillOrigin = 0;
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
    
    protected virtual void Death()
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
