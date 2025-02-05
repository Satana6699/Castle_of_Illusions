using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CauldronAtack))]
[RequireComponent(typeof(CauldronJumpAtack))]
public class CauldronController : MonoBehaviour
{
    [SerializeField] private float distanceForPiuAttack = 3f;
    [SerializeField] private float attackColldown = 3f;
    
    private CauldronJumpAtack _jumpAtack;
    private CauldronAtack _atack;
    private float _timerAttackColldown = 0f;


    private void Start()
    {
        _jumpAtack = GetComponent<CauldronJumpAtack>();
        _atack = GetComponent<CauldronAtack>();
        _timerAttackColldown = attackColldown;
    }

    private void Update()
    {
        _timerAttackColldown += Time.deltaTime;
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            var distance = Vector3.Distance(transform.position, other.transform.position);
            Debug.Log("distance: " + distance);
            if (_timerAttackColldown >= attackColldown)
            {
                if (distance >= distanceForPiuAttack)
                {
                    StartCoroutine(_atack.SpawnBomb());
                }
                else
                {
                    StartCoroutine(_jumpAtack.Jump());
                }

                _timerAttackColldown = 0;
            }
        }
    }
}
