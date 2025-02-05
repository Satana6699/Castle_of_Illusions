using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
    {
        [Header("Health Settings")]
        [SerializeField] protected float health = 100f;
        private float _maxHealth;

        [Header("Invulnerability Settings")]
        [SerializeField] private float invulnerableDuration = 0.5f;
        [SerializeField] private float blinkSpeed = 5f;
        [SerializeField] private string playerLayer = "Player"; 
        [SerializeField] private string enemyLayer = "Enemy"; 
        [SerializeField] private float timeInvulnerable = 0f;
        private bool _isInvulnerable = false;

        [Header("UI Elements")]
        [SerializeField] protected Image healthBar;

        [Header("Effects")]
        [SerializeField] private Renderer render;
        //private RollingEffect _rollingEffect;
        
        [Header("GameManager")]
        [SerializeField] protected GameManager gameManager;
        
        void Start()
        {
            _maxHealth = health;
            //_rollingEffect = GetComponent<RollingEffect>();
        }

        void Update()
        {
            InvulnerableEffect();
        
            if (_isInvulnerable)
            {
                timeInvulnerable += Time.deltaTime;
            }

            if (timeInvulnerable >= invulnerableDuration)
            {
                _isInvulnerable = false;
                timeInvulnerable = 0f;
            }
        }
        
        public void TakeDamage(float damage)
        {
            if (_isInvulnerable)
            {
                return;
            }
        
            //if (_rollingEffect != null && _rollingEffect.CheckRoll())
            //{
            //    return;
            //}
            
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
                    /*Physics.IgnoreLayerCollision(LayerMask.NameToLayer(playerLayer), LayerMask.NameToLayer(enemyLayer),
                        true);*/
                    color.a = Mathf.PingPong(Time.time * blinkSpeed, 0.5f) + 0.5f;
                    material.color = color;
                }
            }
            else
            {
                foreach (var material in render.materials)
                {
                    Color color = material.color;
                    Physics.IgnoreLayerCollision(LayerMask.NameToLayer(playerLayer), LayerMask.NameToLayer(enemyLayer),
                        false);
                    color.a = 1f;
                    material.color = color;
                }
            }
        }
        
        protected virtual void Death()
        {
            gameManager?.EndGame();
            Destroy(gameObject);
        }
    }