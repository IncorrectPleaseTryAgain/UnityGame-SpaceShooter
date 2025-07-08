using UnityEngine;
using System;

[Tooltip("Defines the actions that can be performed by buttons in the game.")]
public enum Actions
{
    None,
    LoadScene,

    // Settings Actions
    OpenSettings,
    CloseSettings,
    SaveSettings,
    ResetSettingsAudio,
    ResetSettingsControls,
    ResetSettingsVideo,

    QuitGame
}

public class ButtonLogic : MonoBehaviour
{
    const string _logTag = "ButtonLogic";

    [Header("Properties")]
    [SerializeField] Actions action;
    [SerializeField] Scenes scene;

    public static event Action<Actions> OnButtonAction;

    public void ExecuteAction()
    {
        LogSystem.Instance.Log($"Executing action: {action}", LogType.Info, _logTag);
        OnButtonAction?.Invoke(action);

        switch (action)
        {
            case Actions.LoadScene:
                LoadScene();
                break;
            case Actions.QuitGame:
                QuitGame();
                break;
        }
    }

    //private void LoadChapterLevel()
    //{
    //}

    private void LoadScene()
    {
        if (scene == Scenes.None)
        {
            LogSystem.Instance.Log("No scene defined to load.", LogType.Warning, _logTag);
            return;
        }
        //if (scene == Scene.PreviousScene)
        //{
        //    LogSystem.Instance.Log("Cannot load previous scene directly. Temporarily redirects to main menu", LogType.Todo, _logTag);
        //    string mainMenu = Enum.GetName(typeof(Scene), Scene.Scene_MainMenu);
        //    UnityEngine.SceneManagement.SceneManager.LoadScene(mainMenu);
        //}

        //string sceneName = Enum.GetName(typeof(Scene), scene);
        //LogSystem.Instance.Log($"Loading scene: {sceneName}");
        //UnityEngine.SceneManagement.SceneManager.LoadScene(sceneName);

        SceneSystem.Instance.LoadScene(scene);
    }

    private void QuitGame()
    {
        LogSystem.Instance.Log("Quitting game...");
        Application.Quit();

        // If running in the editor, stop playing
        #if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
        #endif
    }
}
