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
            if (other.CompareTag("Player"))
            {
                other.gameObject.GetComponent<PlayerHealth>().TakeDamage(damage);
            }
        }
    }
}
