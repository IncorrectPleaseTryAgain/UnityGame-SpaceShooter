using UnityEditor.Animations;
using UnityEngine;

[CreateAssetMenu(fileName = "DynamicEnemy", menuName = "Scriptable Objects/DynamicEnemy")]
public class DynamicEnemy : ScriptableObject
{
    [Header("Properties")]
    public string Name;
    public string Description;
    public float Health;
    public float Damage;
    public float MaxSpeed;

    [Header("Components")]
    public Sprite Sprite;
    public AnimatorController AnimatorController;

    [Header("Effects")]
    public AudioClip ACLP_Death;
    public AudioClip ACLP_Attack;
    public AudioClip ACLP_Hit;
    public AudioClip ACLP_PlayerHit;
}
