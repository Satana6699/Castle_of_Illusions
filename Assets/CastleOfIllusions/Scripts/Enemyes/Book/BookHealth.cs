using UnityEngine;

public class BookHealth : EnemyHealth
{
    [SerializeField] private GameSettings gameSettings;

    protected override void Start()
    {
        if (gameSettings is not null)
        {
            health = gameSettings.bookHealth;
        }
        
        base.Start();
    }
}