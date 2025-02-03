using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
    {
        [Header("Health Settings")]
        [SerializeField] protected float health = 100f;
        [SerializeField] private float maxHealth;

        [Header("Invulnerability Settings")]
        [SerializeField] private float invulnerableDuration = 0.5f;
        [SerializeField] private float blinkSpeed = 5f;
        [SerializeField] private bool isInvulnerable = false;
        [SerializeField] private float timeInvulnerable = 0f;

        [Header("UI Elements")]
        [SerializeField] protected Image healthBar;

        [Header("Effects")]
        [SerializeField] private Renderer render;
        private RollingEffect _rollingEffect;

        void Start()
        {
            maxHealth = health;
            _rollingEffect = GetComponent<RollingEffect>();
        }

        void Update()
        {
            InvulnerableEffect();
        
            if (isInvulnerable)
            {
                timeInvulnerable += Time.deltaTime;
            }

            if (timeInvulnerable >= invulnerableDuration)
            {
                isInvulnerable = false;
                timeInvulnerable = 0f;
            }
        }
        
        public void TakeDamage(float damage)
        {
            if (isInvulnerable)
            {
                return;
            }
        
            if (_rollingEffect != null && _rollingEffect.CheckRoll())
            {
                return;
            }
            
            isInvulnerable = true;
            
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
            healthBar.fillAmount = health / maxHealth;
        }
        
        private void InvulnerableEffect()
        {
            if (render is null)
            {
                return;
            }
        
            if (isInvulnerable)
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