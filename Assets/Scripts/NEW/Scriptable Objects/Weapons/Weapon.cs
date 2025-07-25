using UnityEngine;

[CreateAssetMenu(fileName = "Weapon", menuName = "Scriptable Objects/Weapon")]
public class Weapon : ScriptableObject
{
    [Header("Weapon")]
    public GameObject Prefab;

    [Header("Weapon Properties")]
    public float Damage;
    public bool Automatic;
    [Tooltip("How many entities should be spawned per instance of attack")]
    public float NumItemsPerAttack;
    [Tooltip("IF Automatic -> time between shots (Seconds)")]
    public float AutomaticAttackDelay;
    [Tooltip("Seconds")]
    public float Lifetime;

    [Header("Weapon Sounds")]
    public AudioClip FireSound;
    public AudioClip ReloadSound;

    [Header("Weapon Effects")]
    public GameObject MuzzleFlashEffect;
}