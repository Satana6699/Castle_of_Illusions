using UnityEngine;

public class BossHealth : EnemyHealth
{
    [SerializeField] private GameSettings gameSettings;
    [SerializeField] private DoorOpen doorOpen;
    
    protected override void Start()
    {
        if (gameSettings)
        {
            Health = gameSettings.bossHealth;
        }
            
        base.Start();
    }

    protected override void Death()
    {
        doorOpen.OpenDoor();
        
        base.Death();
    }
}