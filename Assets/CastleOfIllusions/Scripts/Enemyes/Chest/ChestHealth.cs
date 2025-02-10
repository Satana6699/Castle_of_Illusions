using UnityEngine;

public class ChestHealth : EnemyHealth
{
    [SerializeField] private GameSettings gameSettings;

    protected override void Start()
    {
        if (gameSettings)
        {
            Health = gameSettings.chestHealth;
        }
        
        base.Start();
    }
    
    protected override void UpdateFillOrigin()
    {
        float yRotation = transform.eulerAngles.y;
        float yRotationError = 5f;
            
        if ((yRotation < -90 + yRotationError && yRotation > -90 - yRotationError || 
             yRotation < 270 + yRotationError && yRotation > 270 - yRotationError))
        {
            healthBar.fillOrigin = 0;
        }
        else if ((yRotation < 90 + yRotationError && yRotation > 90 - yRotationError || 
                  yRotation < -270 + yRotationError && yRotation > -270 - yRotationError))
        {
            healthBar.fillOrigin = 1;
        }
    }
}