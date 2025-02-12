using UnityEngine;

[CreateAssetMenu(fileName = "SoundSettings", menuName = "Settings/Game Settings")]
public class SoundSettings : ScriptableObject
{
    // 1 Фоновая музыка
    public AudioClip backgroundMusic;
    
    [Header("Player Sounds")]
    // 2 Звук передвижения игрока
    public AudioClip playerMovementSound; //

    // 3 Звук прыжка игрока
    public AudioClip playerJumpSound; //

    // 4 Звук атаки мечом
    public AudioClip swordAttackSound; //

    // 5 Звук попадания мечом
    public AudioClip swordHitSound; //

    // 19 Звук получения урона игровым персонажем
    public AudioClip playerDamageSound; //

    // 20 Звук смерти игрового персонажа
    public AudioClip playerDeathSound; //

    // 21 Звук лечения
    public AudioClip healthPickupSound; //

    // 22 Звук кувырка игрового персонажа
    public AudioClip playerRollSound; //
    
    [Header("Book Sounds")]
    // 8 Звук летающей книги
    public AudioClip flyingBookSound; //

    // 6 Звук атаки книги
    public AudioClip attackBookSound; //?
    
    [Header("Chest Sounds")]
    // 7 Звук укуса мимика
    public AudioClip chestAttackSound;//

    [Header("Chair Sounds")]
    // 9 Звук атаки табуретки
    public AudioClip chairAttackSound; //

    [Header("UI Sounds")]
    // 10 Звук взаимодействия с UI
    public AudioClip uiInteractionSound; //

    // 11 Звук выигрыша
    public AudioClip winSound;//
    
    // 25? Звук деактивации ловушки шипов
    //public AudioClip deactivateSpikesTrapSound;
    
    [Header("Boss Sounds")]
    // 12 Звук передвижения котла
    public AudioClip cauldronMovementSound;//

    // 13 Звук дальней атаки кртла
    public AudioClip distantAttackSound;//

    // 14 Звук прыжка котла
    public AudioClip boilerJumpSound;//

    // 15 Звук ближней атаки котла
    //public AudioClip boilerCloseAttackSound;

    // 26? Фоновая музыка босс-файта
    //public AudioClip bossFightBackgroundMusic;
    
    [Header("trap Sounds")]
    //  16 Звук пилы
    public AudioClip sawSound; // 

    // 17 Звук шипов
    public AudioClip spikesSound; //

    //  18 Звук полёта дротиков
    public AudioClip dartFlightSound; //

    // 23 Звук открывания двери
    public AudioClip doorOpenSound; 

    // 24? Звук подъёма на лестнице
    public AudioClip ladderClimbSound;
    
}