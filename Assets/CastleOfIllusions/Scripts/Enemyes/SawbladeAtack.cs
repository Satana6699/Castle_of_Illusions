using System;
using System.Collections;
using System.Collections.Generic;
using CastleOfIllusions.Scripts;
using UnityEngine;

public class SawbladeAtack : MonoBehaviour
{
    [SerializeField] private float damage = 10f;

        private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.gameObject.GetComponent<PlayerHealth>().TakeDamage(damage);
        }
    }
}
