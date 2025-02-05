using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.Serialization;

public class SawbladeObject : MonoBehaviour
{
    [Header("Animation Settings")]
    [SerializeField] private float damage = 1f;
    [SerializeField] private float animationDuration = 1f;

    [Header("Movement Settings")]
    [SerializeField] private float speedMove = 2f;
    private float _moveInput = 1f;

    private Tween _tween = null;
    private Vector3 _rotation = Vector3.zero;
    
    public float MoveInput
    {
        get => _moveInput;
        set
        {
            _moveInput = Math.Clamp(value, -1, 1);
            TurnRotate();
        }
    }
    void Start()
    {
        _rotation = new Vector3(0, 0, 180);
        
        // Если пила двигается вправо то угол вращения -180
        _tween = transform.DORotate(_rotation * -_moveInput, animationDuration, RotateMode.LocalAxisAdd)
            .SetEase(Ease.Linear)
            .SetLoops(-1, LoopType.Restart);
    }

    void Update()
    {
        transform.position += Vector3.right * _moveInput * speedMove * Time.deltaTime;
        
    }
    
    private void TurnRotate()
    {
        if (_tween is not null)
        {
            _tween.Kill();
        }

        _tween = transform.DORotate(_rotation * -_moveInput, animationDuration, RotateMode.LocalAxisAdd)
            .SetEase(Ease.Linear)
            .SetLoops(-1, LoopType.Restart);
    }
}
