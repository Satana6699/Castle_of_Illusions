using System;
using CastleOfIllusions.Scripts;
using UnityEngine;
using UnityEngine.Serialization;

public class Bullet : MonoBehaviour
{
    [SerializeField] private GameSettings gameSettings;
    
    private float _damage = 10f;
    private float _speed = 1f;
    private float _timeLive = 2f;

    [Header("Particle Settings")]
    [SerializeField] private new ParticleSystem particleSystem = null;

    private float _timerLive = 0f;

    private void Start()
    {
        if (gameSettings)
        {
            _damage = gameSettings.bookDamage;
            _speed = gameSettings.speedBulletBook;
            _timeLive = gameSettings.timeLiveBulletBook;
        }
    }

    private void Update()
    {
        _timerLive += Time.deltaTime;
        
        if (_timerLive >= _timeLive)
        {
            Death();
        }
        
        transform.Translate(Vector3.forward * _speed * Time.deltaTime);
    }

    private void Death()
    {
        Instantiate(particleSystem, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            var playerHealth = other.gameObject.GetComponent<PlayerHealth>();
            playerHealth?.TakeDamage(_damage);
        }
        
        Death();
    }
}