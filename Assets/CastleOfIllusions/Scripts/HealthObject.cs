using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class HealthObject : MonoBehaviour
{
    [SerializeField] private float addHealth = 10f;
    [SerializeField] private float durationAnimation = 1f;

    private Sequence _sequence;
    private Tween _tween;
    
    private void Start()
    {
        _sequence = DOTween.Sequence();

        _sequence.Append(transform.DORotate(new Vector3(0f, 360f, 0f), durationAnimation, RotateMode.LocalAxisAdd)
            .SetEase(Ease.Linear)
            .SetLoops(-1, LoopType.Restart));

        _sequence.Append(transform.DOLocalMoveY(transform.position.y + 0.5f, 0.5f) // Подпрыгивание
            .SetLoops(-1, LoopType.Yoyo)
            .SetEase(Ease.InOutSine));

        _sequence.Play();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            var playerHealth = other.GetComponent<PlayerHealth>();
            playerHealth.Heal(addHealth);
            Destroy(gameObject);
        }
    }

    private void OnDestroy()
    {
        _tween.Kill();
        _sequence.Kill();
    }
}
