using UnityEngine;

[CreateAssetMenu(fileName = "SoundSettings", menuName = "Settings/Game Settings")]
public class SoundSettings : ScriptableObject
{
    // Фоновая музыка
    public AudioClip backgroundMusic;
    
    [Header("Player Sounds")]
    // Звук передвижения игрока и противников
    public AudioClip playerMovementSound;

    // Звук прыжка игрока
    public AudioClip playerJumpSound;

    // Звук атаки мечом
    public AudioClip swordAttackSound;

    // Звук попадания мечом
    public AudioClip swordHitSound;

    // Звук получения урона игровым персонажем
    public AudioClip playerDamageSound;

    // Звук смерти игрового персонажа
    public AudioClip playerDeathSound;

    // Звук подбора хилки и лечения
    public AudioClip healthPickupSound;

    // Звук кувырка игрового персонажа
    public AudioClip playerRollSound;
    
    [Header("Book Sounds")]
    // Звук атаки летающей книги
    public AudioClip flyingBookAttackSound;

    // Звук парения книги
    public AudioClip floatingBookSound;
    
    [Header("Chest Sounds")]
    // Звук укуса мимика
    public AudioClip mimicBiteSound;

    [Header("Chair Sounds")]
    // Звук атаки табуретки
    public AudioClip stoolAttackSound;

    [Header("UI Sounds")]
    // Звук взаимодействия с UI
    public AudioClip uiInteractionSound;

    // Звук выигрыша
    public AudioClip winSound;
    
    // Звук деактивации ловушки шипов
    public AudioClip deactivateSpikesTrapSound;
    
    [Header("Boss Sounds")]
    // Звук передвижения котла
    public AudioClip boilerMovementSound;

    // Звук дальней атаки
    public AudioClip distantAttackSound;

    // Звук прыжка котла
    public AudioClip boilerJumpSound;

    // Звук ближней атаки котла
    public AudioClip boilerCloseAttackSound;

    // Фоновая музыка босс-файта
    public AudioClip bossFightBackgroundMusic;
    
    [Header("trap Sounds")]
    // Звук пилы
    public AudioClip sawSound;

    // Звук шипов
    public AudioClip spikesSound;

    // Звук полёта дротиков
    public AudioClip dartFlightSound;

    // Звук открывания двери
    public AudioClip doorOpenSound;

    // Звук подъёма на лестнице
    public AudioClip ladderClimbSound;
}