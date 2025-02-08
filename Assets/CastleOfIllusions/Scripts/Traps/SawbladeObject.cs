using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.Serialization;

public class SawbladeObject : MonoBehaviour
{
    [SerializeField] private GameSettings gameSettings;
    
    private float _animationDuration = 1f;
    private float _speedMove = 2f;
    private float _moveInput = 1f;

    private Tween _tween = null;
    private Vector3 _rotation = Vector3.zero;
    
    void Start()
    {
        if (gameSettings)
        {
            _animationDuration = gameSettings.animationDurationSawbladeRotation;
            _speedMove = gameSettings.speedMoveSawblade;
        }
        
        _rotation = new Vector3(0, 0, 180);
        
        // Если пила двигается вправо то угол вращения -180
        _tween = transform.DORotate(_rotation * -_moveInput, _animationDuration, RotateMode.LocalAxisAdd)
            .SetEase(Ease.Linear)
            .SetLoops(-1, LoopType.Restart);
    }
    
    public float MoveInput
    {
        get => _moveInput;
        set
        {
            _moveInput = Math.Clamp(value, -1, 1);
            TurnRotate();
        }
    }

    void Update()
    {
        transform.position += Vector3.right * _moveInput * _speedMove * Time.deltaTime;
        
    }
    
    private void TurnRotate()
    {
        if (_tween != null)
        {
            _tween.Kill();
        }

        _tween = transform.DORotate(_rotation * -_moveInput, _animationDuration, RotateMode.LocalAxisAdd)
            .SetEase(Ease.Linear)
            .SetLoops(-1, LoopType.Restart);
    }
}
