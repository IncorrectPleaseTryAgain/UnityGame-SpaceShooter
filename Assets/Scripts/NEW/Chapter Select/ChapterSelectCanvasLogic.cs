using TMPro;
using UnityEngine.UI;
using UnityEngine;
using System;

public class ChapterSelectCanvasLogic : MonoBehaviour
{
    private void OnDestroy()
    {
        SettingsManager.OnSettingsOpened -= OnSettingsOpenedHandler;
        SettingsManager.OnSettingsClosed -= OnSettingsClosedHandler;
    }
    private void Awake()
    {
        SettingsManager.OnSettingsOpened += OnSettingsOpenedHandler;
        SettingsManager.OnSettingsClosed += OnSettingsClosedHandler;
    }

    public void OnMainMenuButtonClicked()
    {
        AudioSystem.Instance.StopMusic();   
        SceneSystem.Instance.LoadScene(Scenes.MainMenu);
    }
    public void OnSettingsOpenedHandler()
    {
        gameObject.SetActive(false);
    }

    public void OnSettingsClosedHandler()
    {
        gameObject.SetActive(true);
    }
}
