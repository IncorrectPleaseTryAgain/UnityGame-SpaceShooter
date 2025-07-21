using UnityEditor.Animations;
using UnityEngine;

[CreateAssetMenu(fileName = "Spaceship", menuName = "Scriptable Objects/Spaceship")]
public class Spaceship : ScriptableObject
{
    [Header("Spaceship")]
    public string Name;
    public string Description;
    public Sprite Sprite;
    public AnimatorController AnimatorController;

    [Header("Spaceship Properties")]
    public float MaxHealth;
    public float MaxSpeed;
    public float GravityScale;
}
