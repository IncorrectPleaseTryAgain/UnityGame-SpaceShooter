using UnityEngine;

[CreateAssetMenu(fileName = "Level Data", menuName = "Scriptable Objects/LevelData")]
public class LevelData : ScriptableObject
{
    [Header("Level Information")]
    public int Chapter;
    public int Level;

    [Header("Game Rules")]
    public int NumberOfEnemies;
    public int NumberOfBosses;
    [Tooltip("Seconds")]
    public float TimeLimit;

    [Header("Spaceship")]
    public Spaceship spaceship;

    [Header("Enemies")]
    public GameObject[] StaticEnemies;
    public GameObject[] DynamicEnemies;
    public GameObject Boss;

}
