using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

public class ChanceDropHealth : MonoBehaviour
{
    [SerializeField] private GameSettings gameSettings;
    
    private float _chanceDrop = 1f;
    
    [SerializeField] private GameObject healthGameObject;

    private void Start()
    {
        if (gameSettings)
        {
            _chanceDrop = gameSettings.chanceDropHealth;
        }
    }
    
    private bool Chance(float percent) {
        return Random.value <= percent / 100f;
    }

    public void SpawnHealInChance()
    {
        if (Chance(_chanceDrop)) {
            Instantiate(healthGameObject, transform.position, Quaternion.identity);
        }
    }
}
