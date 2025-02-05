using System;
using System.Collections;
using System.Collections.Generic;
using CastleOfIllusions.Scripts;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class SawbladeAtack : MonoBehaviour
{
    [SerializeField] private float damage = 10f;
    [SerializeField] private float damageInterval  = 0.3f;
    
    private float _timerDamageInterval = 0f;

    private void Start()
    {
        _timerDamageInterval = damageInterval;
    }

    private void Update()
    {
        _timerDamageInterval += Time.deltaTime;
    }
    
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (_timerDamageInterval >= damageInterval)
            {
                var player = other.GetComponent<PlayerHealth>();
                player?.TakeDamage(damage);
                _timerDamageInterval = 0;
            }
        }
    }
}
