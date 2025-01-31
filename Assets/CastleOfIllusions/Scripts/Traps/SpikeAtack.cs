using UnityEngine;

namespace CastleOfIllusions.Scripts
{
    public class SpikeAtack : MonoBehaviour
    {
        [SerializeField] private float damage = 10f;
    
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                other.GetComponent<PlayerHealth>().TakeDamage(damage);
            }
        }
    }
}
