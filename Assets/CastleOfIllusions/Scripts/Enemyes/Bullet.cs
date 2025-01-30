using System;
using System.Collections;
using CastleOfIllusions.Scripts;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float damage = 10f;
    [SerializeField] private float speed = 1f;
    [SerializeField] private float timeLive = 2f;
    [SerializeField] private new ParticleSystem particleSystem = null;

    private MeshRenderer _meshRenderer = null;
    private float _timerLive = 0f;
    private bool _isDead = false;
    
    private void Start()
    {
        _meshRenderer = GetComponent<MeshRenderer>();
    }
    
    private void Update()
    {
        _timerLive += Time.deltaTime;
        
        if (_timerLive >= timeLive)
        {
            StartCoroutine(Death());
        }
        
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }

    private IEnumerator Death()
    {
        if (_isDead)
            yield break;
        
        _isDead = true;
        _meshRenderer.enabled = false;
        var particle = Instantiate(particleSystem, transform.position, Quaternion.identity);
        yield return new WaitForSeconds(particleSystem.main.duration);
        particle.Stop();
        Destroy(particle);
        Destroy(gameObject);
    }
    
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Bullet ^ OnTriggerEnter");
        
        if (other.CompareTag("Player"))
        {
            var playerStats = other.gameObject.GetComponent<PlayerHealth>();
            playerStats?.TakeDamage(damage);
        }
        
        StartCoroutine(Death());
    }
}