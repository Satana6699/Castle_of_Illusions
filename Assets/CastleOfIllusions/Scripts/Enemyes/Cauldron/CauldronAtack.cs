using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class CauldronAtack : MonoBehaviour
{
    [Header("Settings Distance Atack")]
    [SerializeField] private Bomb bombPrefab;
    [SerializeField] private float height = 10f;
    [SerializeField] private GameObject player;
    [SerializeField] private Transform piuTransform;
    
    
    public IEnumerator SpawnBomb()
    {
        yield return null;
        Instantiate(bombPrefab, piuTransform.position, Quaternion.identity).Initialize(player.transform, height);
    }
}
