using UnityEngine;

[CreateAssetMenu(fileName = "PrimaryWeapon", menuName = "Scriptable Objects/PrimaryWeapon")]
public class PrimaryWeapon : ScriptableObject
{
    [Header("Properties")]
    public string Name;
    public string Description;
    public float Lifetime;
    public float Damage;
    public float Speed;

    [Header("Components")]
    public Sprite Sprite;

    [Header("Effects")]
    public AudioClip ACLP_Fire;
}
