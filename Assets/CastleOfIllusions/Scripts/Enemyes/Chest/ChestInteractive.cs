using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestInteractive : MonoBehaviour
{
    [SerializeField] private GameObject chestLive;
    [SerializeField] private GameObject interactiveCanvas;
    [SerializeField] private ParticleSystem particleSystem;

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            interactiveCanvas.SetActive(true);

            if (Input.GetKey(KeyCode.E))
            {
                Instantiate(particleSystem, transform.position, Quaternion.identity);
                chestLive.SetActive(true);
                Destroy(gameObject);
            }
        }
            
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            interactiveCanvas.SetActive(false);
        }
    }
    
    
}
