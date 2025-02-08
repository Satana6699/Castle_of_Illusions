using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class CauldronDistanceAtack : MonoBehaviour
{
    [SerializeField] private GameSettings gameSettings;
    
    [Header("Settings Distance Atack")]
    [SerializeField] private Bomb bombPrefab;
    [SerializeField] private GameObject player;
    [SerializeField] private Transform piuTransform;
    private float _height = 10f;

    private void Start()
    {
        if (gameSettings)
        {
            _height = gameSettings.bossHeightDistanceAttack;
        }
    }

    public IEnumerator SpawnBomb()
    {
        yield return null;
        Instantiate(bombPrefab, piuTransform.position, Quaternion.identity).Initialize(player.transform, _height);
    }
}
