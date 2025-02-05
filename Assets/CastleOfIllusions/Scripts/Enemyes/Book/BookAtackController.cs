using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BookAtackController : MonoBehaviour
{
    [SerializeField] private GameObject bulletPrefab = null;
    [SerializeField] private GameObject player = null;
    [SerializeField] private Vector3 offsetVector = new Vector3(0,0,0);
    
    [SerializeField] private float timeBetweenShots = 0.2f;
    [SerializeField] private float timeBetweenQueue = 2f;
    [SerializeField] private int countBullet = 3;
    
    private float _timerQueue = 0f;
    private bool _canAttack = true;
    
    void Start()
    {
        timeBetweenQueue += timeBetweenShots;
    }

    void Update()
    {
        if (!_canAttack)
        {
            _timerQueue += Time.deltaTime;
        }

        if (_timerQueue >= timeBetweenQueue)
        {
            _canAttack = true;
            _timerQueue = 0f;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (_canAttack)
            {
                StartCoroutine(Attack());
            }
        }
    }

    private IEnumerator Attack()
    {
        _canAttack = false;

        for (int i = 0; i < countBullet; i++)
        {
            if (player is null)
            {
                player = GameObject.FindGameObjectWithTag("Player");
            }
            
            if (player is not null)
            {
                Vector3 direction = player.transform.position + offsetVector - gameObject.transform.position;
                Quaternion bulletRotation = Quaternion.LookRotation(direction);
                Instantiate(bulletPrefab, transform.position, bulletRotation);
            }
            
            yield return new WaitForSeconds(timeBetweenShots);
        }
        
        _canAttack = false;
    }
}
