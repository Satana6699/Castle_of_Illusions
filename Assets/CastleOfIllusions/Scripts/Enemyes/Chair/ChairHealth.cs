using UnityEngine;

public class ChairHealth : EnemyHealth
{
    [SerializeField] private GameSettings gameSettings;

    protected override void Start()
    {
        if (gameSettings)
        {
            Health = gameSettings.chairHealth;
        }
        
        base.Start();
    }
}