using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DartTrap : MonoBehaviour
{
    [Header("Bullet Settings")]
    [SerializeField] private GameObject bullet;
    [SerializeField] private GameObject bulletStartPosition;

    [Header("Trap Settings")]
    [SerializeField] private float coolDownTrap = 2f;
    private float _timerCoolDownTrap = 0f;

    private bool _isActive = false;
    private Vector3 _startPosition;

    void Start()
    {
        _startPosition = transform.position;
    }

    void Update()
    {
        if (_isActive)
        {
            _timerCoolDownTrap += Time.deltaTime;

            if (_timerCoolDownTrap >= coolDownTrap)
            {
                _timerCoolDownTrap = 0f;
                _isActive = false;
                transform.position = _startPosition;
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (_isActive)
        {
            return;
        }
        
        if (other.CompareTag("Player"))
        {
            var bulletInstantiate = 
                Instantiate(bullet, bulletStartPosition.transform.position, bulletStartPosition.transform.rotation);
            _isActive = true;
            transform.position -= new Vector3(0f, 0.02f, 0f);
        }
    }
}
