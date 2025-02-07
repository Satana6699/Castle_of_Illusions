using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu(fileName = "GameSettings", menuName = "Settings/Game Settings")]
public class GameSettings : ScriptableObject
{
    [Header("Player Stats")]
    public float playerHealth = 100f;
    public float playerDamage = 20f;
    public float playerSpeedAttck = 1f;
    public float playerSpeed = 5f;
    public float playerGravityForce = -35f;
    public float playerJumpForce = 7f;

    [Header("Chair Stats")]
    public float chairHealth = 50f;
    public float chairDamage = 20f;
    public float chairSpeed = 3f;
    public float chairGravityForce = -30f;

    [Header("Book Stats")]
    public float bookHealth;
    
    
    [Header("Boss Stats")]
    public float bossHealth = 50f;
    public float bossDamage = 10f;
    public float bossJumpForce = 3f;
    public float bossGravityForce = -30f;
    public float distanceForDistanceAttack = 5f;
    public float bossAttackCooldown = 3f;
    
    [Header("Traps Stats")]
    public float trapDamage = 1f;

}