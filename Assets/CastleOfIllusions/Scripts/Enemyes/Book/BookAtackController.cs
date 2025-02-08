using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BookAtackController : MonoBehaviour
{
    [SerializeField] private GameSettings gameSettings;
    
    [SerializeField] private GameObject bulletPrefab = null;
    [SerializeField] private GameObject player = null;
    private Vector3 _offsetVector = new Vector3(0,0,0);
    
    private float _timeBetweenShots = 0.2f;
    private float _timeBetweenQueue = 2f;
    private int _countBullet = 3;
    
    private float _timerQueue = 0f;
    private bool _canAttack = true;
    
    void Start()
    {
        if (gameSettings is not null)
        {
            _timeBetweenShots = gameSettings.bookTimeBetweenShots;
            _timeBetweenQueue = gameSettings.bookTimeBetweenQueue;
            _countBullet = gameSettings.countBullet;
            _offsetVector = gameSettings.bookOffsetVectorForTarget;
        }

        player ??= GameObject.FindGameObjectWithTag("Player");
        
        _timeBetweenQueue += _timeBetweenShots;
    }

    void Update()
    {
        if (!_canAttack)
        {
            _timerQueue += Time.deltaTime;
        }

        if (_timerQueue >= _timeBetweenQueue)
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

        for (int i = 0; i < _countBullet; i++)
        {
            if (!player)
            {
                player = GameObject.FindGameObjectWithTag("Player");
            }

            if (player)
            {
                Vector3 direction = player.transform.position + _offsetVector - gameObject.transform.position;
                Quaternion bulletRotation = Quaternion.LookRotation(direction);
                Instantiate(bulletPrefab, transform.position, bulletRotation);
                AudioManager.Instance.PlaySFXNoRepeat(AudioManager.Instance.soundSettings.attackBookSound);
            }
            
            yield return new WaitForSeconds(_timeBetweenShots);
        }
        
        _canAttack = false;
    }
}
