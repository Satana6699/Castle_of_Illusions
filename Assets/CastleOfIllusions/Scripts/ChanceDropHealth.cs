using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class ChanceDropHealth : MonoBehaviour
{
    [SerializeField] private GameObject healthGameObject;
    [SerializeField] private float chanceDrop = 50;
    
    bool Chance(float percent) {
        return Random.value <= percent / 100f;
    }

    private void OnDestroy()
    {
        if (Chance(chanceDrop)) {
            Instantiate(healthGameObject, transform.position, Quaternion.identity);
        }
    }
}
