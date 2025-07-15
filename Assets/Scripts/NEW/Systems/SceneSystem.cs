using System;
using System.Collections;
using UnityEngine.SceneManagement;

public enum Scenes
{
    None,
    MainMenu,
    ChapterSelect,
}

public class  SceneSystem : Singleton<SceneSystem>, ISystem
{
    const string _logTag = "SceneSystem";
    public static event Action OnSystemInitialized;

    public IEnumerator Initialize()
    {
        LogSystem.Instance.Log("Initializing SceneSystem...", LogType.Info, _logTag);

        if (SceneSystem.Instance == null) { yield return null; }

        OnSystemInitialized?.Invoke();
    }

    public void LoadScene(Scenes scene)
    {
        SceneManager.LoadScene(scene.ToString());
    }
}