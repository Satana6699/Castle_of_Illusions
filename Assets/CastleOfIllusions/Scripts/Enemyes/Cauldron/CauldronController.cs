using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

[RequireComponent(typeof(CauldronDistanceAtack))]
[RequireComponent(typeof(CauldronJumpAtack))]
public class CauldronController : MonoBehaviour
{
    [SerializeField] private GameSettings gameSettings;
    
    private float _distanceForDistanceAttack = 3f;
    private float _attackCooldown = 3f;
    
    private CauldronJumpAtack _jumpAttack;
    private CauldronDistanceAtack _attack;
    private float _timerAttackCooldown = 0f;


    private void Start()
    {
        if (gameSettings)
        {
            _distanceForDistanceAttack = gameSettings.distanceForDistanceAttack;
            _attackCooldown = gameSettings.bossAttackCooldown;
        }
        
        _jumpAttack = GetComponent<CauldronJumpAtack>();
        _attack = GetComponent<CauldronDistanceAtack>();
        _timerAttackCooldown = _attackCooldown;
    }

    private void Update()
    {
        _timerAttackCooldown += Time.deltaTime;
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (other)
                StartCoroutine(Attack(other));
        }
    }

    private IEnumerator Attack(Collider other)
    {
        yield return new WaitForSeconds(0.1f);
        
        var distance = Vector3.Distance(transform.position, other.transform.position);
        
        if (_timerAttackCooldown >= _attackCooldown)
        {
            if (distance >= _distanceForDistanceAttack)
            {
                StartCoroutine(_attack.SpawnBomb());
            }
            else
            {
                StartCoroutine(_jumpAttack.Jump());
            }

            _timerAttackCooldown = 0;
        }
    }
}
