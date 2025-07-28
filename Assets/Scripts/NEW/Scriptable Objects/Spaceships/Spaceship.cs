using UnityEditor.Animations;
using UnityEngine;

[CreateAssetMenu(fileName = "Spaceship", menuName = "Scriptable Objects/Spaceship")]
public class Spaceship : ScriptableObject
{
    // Which chapter is it unlocked
    public int Chapter;


    [Header("Properties")]
    public string Name;
    public string Description;
    public float Health;
    public float MaxSpeed;
    public float Acceleration;

    [Header("Components")]
    public Sprite Idle;
    public AnimatorController AnimatorController;

    [Header("Effects")]
    public AudioClip ACLP_Death;
    public AudioClip ACLP_Attack;
    public AudioClip ACLP_Hit;
    public AudioClip ACLP_PlayerHit;

    [Header("Weapon")]
    public PrimaryWeapon PrimaryWeapon;
}
