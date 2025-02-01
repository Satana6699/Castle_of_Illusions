using System;
using System.Collections;
using System.Collections.Generic;
using CastleOfIllusions.Scripts;
using UnityEngine;

public class Sword : MonoBehaviour
{
    [SerializeField] private float damage;
    
    public int Score { get; private set; } = 0;

    private void OnTriggerEnter(Collider other)
    {
        
        if (other.CompareTag("Enemy"))
        {
            var enemy = other.gameObject.GetComponent<EnemyHealth>();
            
            if (enemy is not null)
            {
                Score += enemy.TakeDamage(damage);
            }
        }
    }
}
