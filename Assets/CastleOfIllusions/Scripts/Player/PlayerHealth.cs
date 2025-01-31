using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.UI;

namespace CastleOfIllusions.Scripts
{
    public class PlayerHealth : MonoBehaviour
    {
        [Header("Health Stats")]
        [SerializeField] protected float health = 100f;
        [SerializeField] protected ParticleSystem damageParticles;
        
        [Header("Invulnerable Stats")]
        [SerializeField] private float invulnerableDuration = 0.5f;
        [SerializeField] private float blinkSpeed = 5f;
        [SerializeField] protected Image healthBar;
        [SerializeField] private Renderer render;
        
        private RollingEffect _rollingEffect;
        private float _maxHealth;
        private bool _isInvulnerable = false;
        private float _timeInvulnerable = 0f;
        
        void Start()
        {
            _maxHealth = health;
            _rollingEffect = GetComponent<RollingEffect>();
        }

        void Update()
        {
            InvulnerableEffect();
        
            if (_isInvulnerable)
            {
                _timeInvulnerable += Time.deltaTime;
            }

            if (_timeInvulnerable >= invulnerableDuration)
            {
                _isInvulnerable = false;
                _timeInvulnerable = 0f;
            }
        }
        
        public void TakeDamage(float damage)
        {
            if (_isInvulnerable)
            {
                return;
            }
        
            if (_rollingEffect != null && _rollingEffect.CheckRoll())
            {
                return;
            }
            
            _isInvulnerable = true;
            
            health -= damage;
        
            if (health <= 0)
            {
                health = 0;
                Death();
            }
            
            UpdateHealthBar();
        }
        
        private void UpdateHealthBar()
        {
            healthBar.fillAmount = health / _maxHealth;
        }
        
        private void InvulnerableEffect()
        {
            if (render is null)
            {
                return;
            }
        
            if (_isInvulnerable)
            {
                foreach (var material in render.materials)
                {
                    Color color = material.color;
                    color.a = Mathf.PingPong(Time.time * blinkSpeed, 0.5f) + 0.5f;
                    material.color = color;
                }
            }
            else
            {
                foreach (var material in render.materials)
                {
                    Color color = material.color;
                    color.a = 1f;
                    material.color = color;
                }
            }
        }
        
        protected virtual void Death()
        {
            Destroy(gameObject);
        }
    }
}
