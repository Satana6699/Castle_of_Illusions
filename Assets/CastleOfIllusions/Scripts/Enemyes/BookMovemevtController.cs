using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class BockMovemevtController : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 1f;
    [SerializeField] private float speedPercanteRandom = 10f;
    [SerializeField] private float startPosX, startPosY, endPosX, endPosY; 
    [SerializeField] private float waitTime;
    
    private Tween _tween;
    private Vector3 _newPos;
    private float _speedAnimation;
    
    void Start()
    {
        NewRandomValue();

        NewTween();
    }

    private void NewRandomValue()
    {
        _newPos = new Vector3(Random.Range(startPosX, endPosX), Random.Range(startPosY, endPosY), 0f);
        float percanteValue = moveSpeed * speedPercanteRandom / 100f;
        float moveRandom = Random.Range(moveSpeed - percanteValue, moveSpeed + percanteValue);
        _speedAnimation = Vector3.Distance(transform.position, _newPos) / moveRandom;
    }

    private IEnumerator NextPoint()
    {
        _tween.Kill();
        
        yield return new WaitForSeconds(waitTime);
        
        NewRandomValue();
        NewTween();
    }

    private void NewTween()
    {
        _tween = transform.DOMove(_newPos, _speedAnimation)
            .SetEase(Ease.Linear)
            .OnComplete(() =>
            {
                StartCoroutine(NextPoint());
            });
    }
    
    void OnDestroy()
    {
        _tween?.Kill();
    }
}
