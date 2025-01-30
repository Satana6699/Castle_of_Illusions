using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.UI;

namespace CastleOfIllusions.Scripts
{
    public class PlayerHealth : MonoBehaviour
    {
        [Header("Health Stats")]
        [SerializeField] private float health = 100f;
        private float _maxHealth;
        [SerializeField] private Image healthBar;
    
        [Header("Invulnerable Stats")]
        [SerializeField] private float invulnerableDuration = 0.5f;
        [SerializeField] private float blinkSpeed = 5f;
        [SerializeField] private Renderer render;
        private bool _isInvulnerable = false;
        private float _timeInvulnerable = 0f;
        [CanBeNull] private RollingEffect _rollingEffect;
    
        public enum KnockbackType
        {
            Linnear,
            Horizontal,
            Parabolic,
            Sqrt
        }
    
        private Dictionary<KnockbackType, System.Func<float, float>> _knockbacks = 
            new Dictionary<KnockbackType, System.Func<float, float>>();
        private Rigidbody _rigidbody;
        private StunEffect _stunEffect;
    

        void Awake()
        {
            _knockbacks.Add(KnockbackType.Linnear, (float x) => x);
            _knockbacks.Add(KnockbackType.Horizontal, (float x) => 0);
            _knockbacks.Add(KnockbackType.Parabolic, (float x) =>  x * x);
            _knockbacks.Add(KnockbackType.Sqrt, Mathf.Sqrt );
        }
    
        void Start()
        {
            _rigidbody = GetComponent<Rigidbody>();
            _stunEffect = GetComponent<StunEffect>();
            _rollingEffect = GetComponent<RollingEffect>();
            _maxHealth = health;
        }

        void Update()
        {
            StunnedEffect();
        
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
        
            if (_rollingEffect is not null && _rollingEffect.CheckRoll())
            {
                return;
            }
        
            health -= damage;
            _isInvulnerable = true;
        
            if (health <= 0)
            {
                health = 0;
            }
        
            UpdateHealthBar();
        }

        /*
    public void TakeDamageAndPushBack(Vector3 hitPoint, float damage, float knockbackForce, KnockbackType knockbackType = KnockbackType.Linnear)
    {
        if (_isInvulnerable)
        {
            return;
        }
        
        TakeDamage(damage);
        
        _stunEffect?.Stunned();

        if (_rigidbody != null)
        {
            Vector3 knockbackDirection = (transform.position - hitPoint).normalized;
            knockbackDirection *= knockbackForce;
            knockbackDirection.y = _knockbacks[knockbackType].Invoke(knockbackDirection.x);
            _rigidbody.AddForce(knockbackDirection, ForceMode.VelocityChange);
        }
    }
    */
    
        private void UpdateHealthBar()
        {
            healthBar.fillAmount = health / _maxHealth;
        }
    
        private void StunnedEffect()
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
    }
}
