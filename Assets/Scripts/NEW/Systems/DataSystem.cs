using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;

public class DataSystem : Singleton<DataSystem>, ISystem
{
    const string _logTag = "GameDataSystem";

    public int currentSave;
    public int currentLevel;
    public int currentChapter;
    public GameData gameData;

    public LevelData[] levelDatas;

    public static event Action OnSystemInitialized;

    // Initialize System
    public IEnumerator Initialize()
    {
        LogSystem.Instance.Log("Initializing GameDataSystem", LogType.Info, _logTag);

        if(VideoSystem.Instance == null) { yield return null; }

        // Initialize Game Data
        currentSave = 0;
        currentLevel = 1;
        currentChapter = 1;
        gameData = new GameData();

        OnSystemInitialized?.Invoke();
    }

    public LevelData GetLevelData()
    {
        foreach (LevelData levelData in levelDatas)
        {
            if (levelData.Chapter == currentChapter && levelData.Level == currentLevel)
            {
                return levelData;
            }
        }

        LogSystem.Instance.Log($"No LevelData found for Chapter {currentChapter} and Level {currentLevel}", LogType.Error, _logTag);
        return null;
    }
}