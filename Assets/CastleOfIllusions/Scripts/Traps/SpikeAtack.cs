using UnityEngine;
using UnityEngine.Serialization;

namespace CastleOfIllusions.Scripts
{
    public class SpikeAtack : MonoBehaviour
    {
        [SerializeField] private GameSettings gameSettings;
        
        private float _damage = 10f;
    
        private void OnTriggerEnter(Collider other)
        {
            if (gameSettings)
            {
                _damage = gameSettings.spikeDamage;
            }
            
            if (other.CompareTag("Player"))
            {
                other.GetComponent<PlayerHealth>().TakeDamage(_damage);
            }
        }
    }
}
