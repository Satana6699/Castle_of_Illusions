using System.Collections;
using System.Collections.Generic;
using CastleOfIllusions.Scripts;
using UnityEngine;

public class ChestLive : MonoBehaviour
{
    [SerializeField] private Canvas canvas;
    [SerializeField] private GameObject chestPrefab;
    [SerializeField] private ParticleSystem particleLive;
    
    
    void Start()
    {
        canvas.enabled = false;
    }
    
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            canvas.enabled = true;
            
            if (Input.GetKey(KeyCode.E))
            {
                Instantiate(chestPrefab, transform.position, Quaternion.identity);
                Instantiate(particleLive, transform.position, Quaternion.identity);
                Destroy(gameObject);
            }
        }
    }
    
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            canvas.enabled = false;
        }
    }
}
