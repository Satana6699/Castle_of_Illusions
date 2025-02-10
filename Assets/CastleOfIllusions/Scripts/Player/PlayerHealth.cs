using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
    {
        [SerializeField] private GameSettings gameSettings;
        
        [Header("Health Settings")]
        private float _health = 100f;
        private float _maxHealth;

        [Header("UI Elements")]
        [SerializeField] protected Image healthBar;

        private RollingEffect _rollingEffect;
        
        [Header("GameManager")]
        [SerializeField] protected GameManager gameManager;
        
        void Start()
        {
            if (gameSettings)
            {
                _health = gameSettings.playerHealth;
            }
            
            _maxHealth = _health;
            _rollingEffect = GetComponent<RollingEffect>();
        }

        
        public void TakeDamage(float damage)
        {
            if (_rollingEffect != null && _rollingEffect.CheckRoll())
            {
                return;
            }
            
            _health -= damage;
            
            AudioManager.Instance?.PlaySFX(AudioManager.Instance?.soundSettings.playerDamageSound);

            if (_health <= 0)
            {
                _health = 0;
                Death();
            }
            
            UpdateHealthBar();
        }

        public void Heal(float addHealth)
        {
            AudioManager.Instance?.PlaySFX(AudioManager.Instance?.soundSettings.healthPickupSound);
            
            var newHealth = _health + addHealth;
            _health = Mathf.Clamp(newHealth, 0, _maxHealth);
            UpdateHealthBar();
        }
        
        private void UpdateHealthBar()
        {
            healthBar.fillAmount = _health / _maxHealth;
        }
        
        protected virtual void Death()
        {
            AudioManager.Instance?.PlaySFXNoRepeat(AudioManager.Instance?.soundSettings.playerDeathSound);

            gameManager?.EndGame();
            Time.timeScale = 0f;
            Destroy(gameObject);
        }
    }