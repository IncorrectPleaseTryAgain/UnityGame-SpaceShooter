using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class PauseMenuCanvasLogic : MonoBehaviour
{
    [SerializeField] Button _buttonSettings;

    public static event Action OnOpenSettings;
    public static event Action OnResumeGame;

    public void OnExitButtonClick()
    {
        SceneSystem.Instance.LoadScene(Scenes.LevelSelect);
    }

    public void OnSettingsButtonClick()
    {
        OnOpenSettings?.Invoke();
    }

    public void OnResumeButtonClick()
    {
        OnResumeGame?.Invoke();
    }
}
