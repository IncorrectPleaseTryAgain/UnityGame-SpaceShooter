using System;
using System.Collections;

public class GameDataSystem : Singleton<GameDataSystem>, ISystem
{
    const string _logTag = "GameDataSystem";
   
    public SaveData CurrentSaveData;

    public LevelData[] levelDatas;


    // Initialize System
    public static event Action OnSystemInitialized;
    public IEnumerator Initialize()
    {
        LogSystem.Instance.Log("Initializing GameDataSystem", LogType.Info, _logTag);

        if(GameDataSystem.Instance == null) { yield return null; }

        //// Initialize Game Data
        //currentSave = 0;
        //currentLevel = 1;
        //currentChapter = 1;
        //SaveData = new SaveData();

        OnSystemInitialized?.Invoke();
    }

    //public LevelData GetLevelData()
    //{
    //    foreach (LevelData levelData in levelDatas)
    //    {
    //        if (levelData.Chapter == currentChapter && levelData.Level == currentLevel)
    //        {
    //            return levelData;
    //        }
    //    }

    //    LogSystem.Instance.Log($"No LevelData found for Chapter {currentChapter} and Level {currentLevel}", LogType.Error, _logTag);
    //    return null;
    //}
}