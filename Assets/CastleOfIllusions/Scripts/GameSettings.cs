using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu(fileName = "GameSettings", menuName = "Settings/Game Settings")]
public class GameSettings : ScriptableObject
{
    [Space(50)]
    [Header("Player Stats")]
    [Space(10)]
    [Header("Battle Stats")]
    public float playerHealth = 100f;
    public float playerDamage = 20f;
    public float playerSpeedAttack = 1f;
    [Space(10)]
    [Header("Move Stats")]
    public float playerSpeed = 5f;
    public float playerGravityForce = -35f;
    public float playerJumpForce = 7f;

    [Space(50)]
    [Header("Chair Stats")]
    [Space(10)]
    [Header("Move Stats")]
    public float chairHealth = 50f;
    public float chairDamage = 20f;
    [Space(10)]
    [Header("Battle Stats")]
    public float chairSpeed = 3f;
    public float chairGravityForce = -30f;

    [Space(50)]
    [Header("Book Stats")]
    [Space(10)]
    [Header("Battle Stats")]
    public float bookHealth = 50f;
    public float bookDamage;
    public float bookTimeBetweenShots = 0.2f;
    public float bookTimeBetweenQueue = 2f;
    public int countBullet = 3;
    [Space(10)]
    [Header("Bullet Stats")]
    public Vector3 bookOffsetVectorForTarget = new Vector3(0, 0, 0);
    public float speedBulletBook;
    public float timeLiveBulletBook;
    [Space(10)]
    [Header("Move Stats")]
    public float bookSpeed;
    public float speedPercentRandom;
    public float waitTime;
    
    [Space(50)]
    [Header("Boss Stats")]
    [Space(10)]
    [Header("Battle Stats")]
    public float bossHealth = 50f;
    public float bossDamage = 10f;
    public float distanceForDistanceAttack = 5f;
    public float bossAttackCooldown = 3f;
    [Space(10)]
    [Header("Move Stats")]
    public float bossMoveSpeed = 5f;
    public float bossJumpForce = 3f;
    public float bossGravityForce = -30f;
    [Space(10)]
    [Header("Distance Attack Stats")]
    public float bossDetectionRadius = 5f;
    public float bossHeightDistanceAttack = 8f;
    public float speedDistanceBossAttack = 8f;
    public float bossMinDistanceToPlayer = 1.2f;
    
    [Space(50)]
    [Header("Traps Stats")]
    [Space(10)]
    [Header("Sawblade Stats")]
    public float damageSawblade = 2f;
    public float speedMoveSawblade = 5f;
    public float animationDurationSawbladeRotation = 1f;
    [Space(10)]
    [Header("Dart Stats")]
    public float coolDownDartTrap = 0.3f;
    public Vector3 offsetDartTrapInActvate = new Vector3(0, 0.05f, 0);
    [Space(10)]
    [Header("Spike Stats")]
    public float timeActivateSpike = 0.3f;
    public float spikeDamage = 10f;
    
    [Space(50)]
    [Header("Bonus Stats")]
    public float chanceDropHealth = 20f;
    public float heal = 20f;
    public float durationAnimationHeal = 1f;
}