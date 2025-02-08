using System;
using System.Collections;
using System.Collections.Generic;
using CastleOfIllusions.Scripts;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.Serialization;

public class SawbladeAtack : MonoBehaviour
{
    [SerializeField] private GameSettings gameSettings;
    
    private float _damage = 10f;
    [SerializeField] private float damageInterval  = 0.3f;
    
    private float _timerDamageInterval = 0f;

    private void Start()
    {
        if (gameSettings)
        {
            _damage = gameSettings.damageSawblade;
        }
        
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
                player?.TakeDamage(_damage);
                _timerDamageInterval = 0;
            }
        }
    }
}
