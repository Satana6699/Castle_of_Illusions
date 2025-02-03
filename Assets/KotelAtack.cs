using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KotelAtack : MonoBehaviour
{
    [Header("Settings Distance Atack")]
    [SerializeField] private Bomb bombPrefab;
    [SerializeField] private float bombColldown = 2f;
    [SerializeField] private float height = 10f;
    [SerializeField] private GameObject player;
    
    void Start()
    {
        StartCoroutine(SpawnBomb());
    }
    
    private IEnumerator SpawnBomb()
    {
        yield return new WaitForSeconds(bombColldown);
        Instantiate(bombPrefab, transform.position, Quaternion.identity).Initialize(player.transform, height);
        StartCoroutine(SpawnBomb());
    }
}
