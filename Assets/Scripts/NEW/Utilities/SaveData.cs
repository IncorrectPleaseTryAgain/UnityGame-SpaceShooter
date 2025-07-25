using System;

//[Serializable]
public class SaveData
{
    public int saveSlotID = 0;
    public string Name = "";
    public string DateCreated = "";
    public int[] ChaptersUnlocked = { 1, 0, 0, 0, 0 };
    public int[] LevelsUnlocked = { 1, 0, 0, 0, 0 }; // Levels within highest chapter

    public bool shieldUnlocked = false;
    public bool secondaryUnlocked = false;
    public bool boostUnlocked = false;
    public bool autoFireUnlocked = false;

    // Health
    public float maxHp = 100;
    public float hpRegenDelay = 5; // seconds
    public float hpRegenSpeed = 10; // #hp per second

    // Primary
    public int primaryAmmo = 10;
    public float primaryDamage = 10;
    public float primaryAmmoRegenSpeed = 10; // #ammo per second
    public float primaryReloadSpeed = 2; // seconds until full reload

    public int secondaryAmmo = 10;
    public float secondaryDamage = 10;
    public float secondaryAmmoRegenSpeed = 10; // #ammo per second
    public float secondaryReloadSpeed = 2; // seconds until full reload

    // Boost
    public float boostDuration = 0.5f; // seconds
    public float boostDelay = 2; // seconds - time between boosts

    // Shield
    public float shieldDurability = 20;
    public float shieldRegenDelay = 5; // seconds - time delay before shield regens
}