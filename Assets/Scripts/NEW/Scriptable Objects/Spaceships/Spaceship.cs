using UnityEditor.Animations;
using UnityEngine;

[CreateAssetMenu(fileName = "Spaceship", menuName = "Scriptable Objects/Spaceship")]
public class Spaceship : ScriptableObject
{
    [Header("Spaceship")]
    public int chapter;
    public string Name;
    public string Description;
    public Sprite Sprite;
    public AnimatorController AnimatorController;

    [Header("Spaceship Properties")]
    public float MaxHealth;
    public float MaxSpeed;
    public float Acceleration;
    public float SprintMultiplier;
    public float GravityScale;
    public Weapon weapon;
}
