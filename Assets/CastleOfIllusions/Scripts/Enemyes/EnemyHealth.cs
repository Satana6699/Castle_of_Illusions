using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.UI;

namespace CastleOfIllusions.Scripts
{
    public class EnemyHealth : MonoBehaviour
    {
        [Header("Health Stats")]
        [SerializeField] protected float health = 100f;
        [SerializeField] protected ParticleSystem damageParticles;
        
        public int TakeDamage(float damage)
        {
            health -= damage;
        
            Instantiate(damageParticles, transform.position, Quaternion.identity);
            
            if (health <= 0)
            {
                health = 0;
                Death();
                return 1;
            }

            return 0;
        }

        private void Death()
        {
            Destroy(gameObject);
        }
    }
}
