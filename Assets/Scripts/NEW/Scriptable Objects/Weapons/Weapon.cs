using UnityEngine;

[CreateAssetMenu(fileName = "Weapon", menuName = "Scriptable Objects/Weapon")]
public class Weapon : ScriptableObject
{
    [Header("Weapon")]
    public GameObject Prefab;

    [Header("Weapon Properties")]
    public float Damage;
    public float FireRate;
    public float Range;
    public float ReloadTime;

    [Header("Weapon Sounds")]
    public AudioClip FireSound;
    public AudioClip ReloadSound;

    [Header("Weapon Effects")]
    public GameObject MuzzleFlashEffect;
}
