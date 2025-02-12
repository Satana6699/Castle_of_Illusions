using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetMove : MonoBehaviour
{
    [SerializeField] private Transform target;

    
    void Update()
    {
        if (target != null)
            transform.position = target.position;
    }
}
