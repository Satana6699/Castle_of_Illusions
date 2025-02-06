using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class HealthObject : MonoBehaviour
{
    [SerializeField] private float addHealth = 10f;
    [SerializeField] private float durationAnimation = 1f;

    private Tween _tween;
    
    private void Start()
    {
        // Создаем последовательность анимаций
        var sequence = DOTween.Sequence();

        // Вращение объекта
        sequence.Append(transform.DORotate(new Vector3(0f, 360f, 0f), durationAnimation, RotateMode.LocalAxisAdd)
            .SetEase(Ease.Linear)
            .SetLoops(-1, LoopType.Restart)); // Бесконечное вращение (вне Sequence)

        // Подпрыгивание объекта
        transform.DOLocalMoveY(transform.position.y + 0.5f, 0.5f) // Подпрыгивание вверх на 0.5 единиц
            .SetLoops(-1, LoopType.Yoyo) // Периодичность — вверх-вниз
            .SetEase(Ease.InOutSine); // Плавный эффект

        // Запуск последовательности
        sequence.Play();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            var playerHealth = other.GetComponent<PlayerHealth>();
            playerHealth.Health(addHealth);
            Destroy(gameObject);
        }
    }

    private void OnDestroy()
    {
        _tween.Kill();
    }
}
