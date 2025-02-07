using UnityEngine;

public class ChairHealth : EnemyHealth
{
    [SerializeField] private GameSettings gameSettings;

    protected override void Start()
    {
        if (gameSettings is not null)
        {
            health = gameSettings.chairHealth;
        }
        
        base.Start();
    }
}