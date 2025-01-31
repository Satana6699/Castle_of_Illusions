using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CastleOfIllusions.Scripts
{
    public class ChairBattleController : MonoBehaviour
    {
        [SerializeField] private float damage = 10f;

        private void OnCollisionEnter(Collision collision)
        {
            Debug.Log("Колизия табуретка");
            if (collision.gameObject.CompareTag("Player"))
            {
                Debug.Log("Колизия табуретка и игрок");
                collision.gameObject.GetComponent<PlayerHealth>().TakeDamage(damage);
            }
        }
    }
}
