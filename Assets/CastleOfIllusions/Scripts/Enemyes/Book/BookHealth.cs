using UnityEngine;

public class BookHealth : EnemyHealth
{
    [SerializeField] private GameSettings gameSettings;

    protected override void Start()
    {
        if (gameSettings)
        {
            Health = gameSettings.bookHealth;
        }
        
        base.Start();
    }
}