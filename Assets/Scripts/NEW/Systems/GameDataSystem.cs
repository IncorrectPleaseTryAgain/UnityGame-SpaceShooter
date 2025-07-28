using System;
using System.Collections;
using UnityEngine;

public class GameDataSystem : Singleton<GameDataSystem>, ISystem
{
    static readonly string _logTag = "GameDataSystem";

    public static SaveData currentSave;
    public static int currentChapter;
    public static int currentLevel;

    [SerializeField] Spaceship[] spaceships;
    [SerializeField] LevelData[] levelsData;

    // Initialize System
    public static event Action OnSystemInitialized;
    public IEnumerator Initialize()
    {
        LogSystem.Instance.Log("Initializing GameDataSystem", LogType.Info, _logTag);

        if(GameDataSystem.Instance == null) { yield return null; }

        currentChapter = 0;
        currentSave = null;

        OnSystemInitialized?.Invoke();
    }

    public Spaceship GetSpaceship()
    {
        foreach (Spaceship spaceship in spaceships) 
        {
            if(spaceship.Chapter == currentChapter) {  return spaceship; }
        }
        return spaceships[0];
    }
    public LevelData GetLevelData()
    {
        foreach (LevelData leveldata in levelsData)
        {
            if (leveldata.Chapter == currentChapter && leveldata.Level == currentLevel) { return leveldata; }
        }
        return levelsData[0];
    }
}