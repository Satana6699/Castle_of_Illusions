using System;
using System.Collections;
using System.Collections.Generic;
using CastleOfIllusions.Scripts;
using UnityEngine;

public class Sword : MonoBehaviour
{
    [Header("Damage Settings")]
    [SerializeField] private GameSettings gameSettings;
    private float _damage = 10f;

    private void Start()
    {
        if (gameSettings is not null)
        {
            _damage = gameSettings.playerDamage;
        }
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            var enemy = other.gameObject.GetComponent<EnemyHealth>();
            
            if (enemy is not null)
            {
                enemy.TakeDamage(_damage);
            }
        }
    }
}
