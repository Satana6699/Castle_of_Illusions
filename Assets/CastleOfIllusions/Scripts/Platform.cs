using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.Serialization;

public class Platform : MonoBehaviour
{
    [SerializeField] private Vector3 nextPosition;
    [SerializeField] private float moveDuration = 2f;
    [SerializeField] private Ease ease = Ease.InOutExpo;
    
    void Start()
    {
        transform.DOMove(nextPosition, moveDuration)
            .SetEase(ease)
            .SetLoops(-1, LoopType.Yoyo); 
    }
    
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.transform.position += Vector3.up * Time.deltaTime * 2f;
        }
    }

}
