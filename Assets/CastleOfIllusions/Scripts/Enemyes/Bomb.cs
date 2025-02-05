using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

[RequireComponent(typeof(Rigidbody))]
public class Bomb : MonoBehaviour
{
    [SerializeField] private float speed = 10f;
    [SerializeField] private ParticleSystem particleBoom;
    
    private float _time;
    private Transform _targetPosition;
    private float _arcHeight = 3f;
    private Vector3 _startPoint;
    private float _timeNoBoom = 0.2f;
    private float _timerTimeNoBoom = 0f;
    private bool _canBoom = false;
    
    public void Initialize(Transform targetPosition, float height)
    {
        _targetPosition = targetPosition;
        _arcHeight = height;
    }

    private void Update()
    {
        _timerTimeNoBoom += Time.deltaTime;

        if (_timerTimeNoBoom >= _timeNoBoom)
        {
            CanBoom();
        }
    }
    
    void Start()
    {
        _startPoint = transform.position;
        if (_targetPosition != null)
        {
            StartCoroutine(MoveProjectile(_targetPosition.position));
        }
    }

    private void CanBoom()
    {
        _canBoom = true;
    }
    
    private IEnumerator MoveProjectile(Vector2 targetPos)
    {
        float duration = Vector2.Distance(_startPoint, targetPos) / speed;
        for (_time = 0; _time <= 1; _time += Time.deltaTime / duration)
        {
            float x = Mathf.Lerp(_startPoint.x, targetPos.x, _time);
            float y = Mathf.Lerp(_startPoint.y, targetPos.y, _time) + _arcHeight * Mathf.Sin(_time * Mathf.PI);
            transform.position = new Vector2(x, y);
            yield return null;
        }
        
        if (_canBoom)
        {
            Explode();
        }
    }

    void Explode()
    {
        Debug.Log("Взрыв!");
        Instantiate(particleBoom, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }

    void OnCollisionEnter(Collision collision)
    {
        if (_canBoom)
        {
            Explode();
        }
    }
}
