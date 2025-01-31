using System;
using System.Collections;
using System.Collections.Generic;
using CastleOfIllusions.Scripts;
using UnityEngine;

public class Sword : MonoBehaviour
{
    [SerializeField] private float damage;
    
    private void OnTriggerEnter(Collider other)
    {
        
        if (other.CompareTag("Enemy"))
        {
            var enemy = other.gameObject.GetComponent<EnemyHealth>();
            enemy?.TakeDamage(damage);
        }
    }
}
