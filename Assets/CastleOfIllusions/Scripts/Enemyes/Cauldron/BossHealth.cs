using UnityEngine;

public class BossHealth : EnemyHealth
{
    [SerializeField] private GameSettings gameSettings;

    protected override void Start()
    {
        if (gameSettings)
        {
            Health = gameSettings.bossHealth;
        }
            
        base.Start();
    }
}