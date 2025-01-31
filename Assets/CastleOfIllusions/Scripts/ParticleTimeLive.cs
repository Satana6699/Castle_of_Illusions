using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleTimeLive : MonoBehaviour
{
    private ParticleSystem _particle;
    
    void Start()
    {
        _particle = GetComponent<ParticleSystem>();
        Invoke(nameof(Death), _particle.main.duration);
    }

    void Death()
    {
        Destroy(gameObject);
    }
}
