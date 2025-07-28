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
    public float maxHpMultiplier = 1f;
    public float hpRegenDelayMultiplier = 1f; // seconds
    public float hpRegenSpeedMultiplier = 1f; // #hp per second

    // Primary
    public float primaryAmmoMultiplier = 1f;
    public float primaryDamageMultiplier = 1f;
    public float primaryAmmoRegenSpeedMultiplier = 1f; // #ammo per second
    public float primaryReloadSpeedMultiplier = 1f; // seconds until full reload

    public float secondaryAmmoMultiplier = 1f;
    public float secondaryDamageMultiplier = 1f;
    public float secondaryAmmoRegenSpeedMultiplier = 1f; // #ammo per second
    public float secondaryReloadSpeedMultiplier = 1f; // seconds until full reload

    // Boost
    public float boostDurationMultiplier = 1f; // seconds
    public float boostDelayMultiplier = 1f; // seconds - time between boosts

    // Shield
    public float shieldDurabilityMultiplier = 1f;
    public float shieldRegenDelayMultiplier = 1f; // seconds - time delay before shield regens
}