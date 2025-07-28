using UnityEngine;

[CreateAssetMenu(fileName = "Level Data", menuName = "Scriptable Objects/LevelData")]
public class LevelData : ScriptableObject
{
    [Header("Level Information")]
    public int Chapter;
    public int Level;

    [Header("Game Rules")]
    public int NumberOfEnemies;
    [Tooltip("Seconds")]
    public float TimeLimit;
    [Tooltip("Seconds")]
    public float StaticEnemySpawnRate;

    [Header("Enemies")]
    public DynamicEnemyLogic[] DynamicEnemies;
    public GameObject[] StaticEnemies;
    public GameObject BossEnemy;
}
