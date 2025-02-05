using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.Serialization;

public class BockMovemevtController : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 1f;
    [SerializeField] private float speedPercanteRandom = 10f;
    [SerializeField] private GameObject left;
    [SerializeField] private GameObject right;
    [SerializeField] private GameObject down;
    [SerializeField] private GameObject up;
    [SerializeField] private float waitTime;
    
    private Tween _tween;
    private Vector3 _newPos;
    private float _speedAnimation;
    private float _startPosX, _startPosY, _endPosX, _endPosY;
    
    
    void Start()
    {
        _startPosX = left.transform.position.x;
        _endPosX = right.transform.position.x;
        _startPosY = down.transform.position.y;
        _endPosY = up.transform.position.y;
        
        Destroy(left);
        Destroy(right);
        Destroy(down);
        Destroy(up);
        
        NewRandomValue();

        NewTween();
    }

    private void NewRandomValue()
    {
        _newPos = new Vector3(Random.Range(_startPosX, _endPosX), Random.Range(_startPosY, _endPosY), 0f);
        float percentValue = moveSpeed * speedPercanteRandom / 100f;
        float moveRandom = Random.Range(moveSpeed - percentValue, moveSpeed + percentValue);
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
