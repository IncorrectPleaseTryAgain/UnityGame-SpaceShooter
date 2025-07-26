using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class ChapterSelectManager : MonoBehaviour
{
    [Header("Utility")]
    [SerializeField] private EventSystem eventSystem;

    [Header("Enviroment")]
    [SerializeField] private new Camera camera;
    [SerializeField] private GameObject background;

    [Header("Chapter Select")]
    [SerializeField] private ChapterSelectCanvasLogic chapterSelect;

    [Header("Settings")]
    [SerializeField] private SettingsManager settings;

    private void OnDestroy()
    {
        SettingsManager.OnSettingsClosed -= OnSettingsClosedHandler;
        SettingsManager.OnSettingsOpened -= OnSettingsOpenedHandler;
    }

    private void Awake()
    {
        SettingsManager.OnSettingsClosed += OnSettingsClosedHandler;
        SettingsManager.OnSettingsOpened += OnSettingsOpenedHandler;

        Instantiate(eventSystem);
        camera = Instantiate(camera);
        Instantiate(background, camera.transform);
        chapterSelect = Instantiate(chapterSelect);
        settings = Instantiate(settings);
    }

    private void OnSettingsClosedHandler()
    {
        chapterSelect.gameObject.SetActive(true);
    }
    private void OnSettingsOpenedHandler()
    {
        chapterSelect.gameObject.SetActive(false);
    }
}
