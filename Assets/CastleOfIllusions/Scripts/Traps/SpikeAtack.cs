using UnityEngine;

namespace CastleOfIllusions.Scripts
{
    public class SpikeAtack : MonoBehaviour
    {
        [SerializeField] private float damage = 10f;
        //[SerializeField] private float knockBackForce = 10f;
    
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                Vector3 hitPoint = other.ClosestPoint(transform.position);
                other.GetComponent<PlayerHealth>().TakeDamage(damage);
            }
        }

    }
}
