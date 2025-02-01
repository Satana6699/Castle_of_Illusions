using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CastleOfIllusions.Scripts
{
    public class ChairBattleController : MonoBehaviour
    {
        [SerializeField] private float damage = 10f;

        private void OnTriggerStay(Collider other)
        {
            Debug.Log("Trigger табуретка");
            if (other.CompareTag("Player"))
            {
                Debug.Log("Trigger табуретка и игрок");
                other.gameObject.GetComponent<PlayerHealth>().TakeDamage(damage);
            }
        }
    }
}
