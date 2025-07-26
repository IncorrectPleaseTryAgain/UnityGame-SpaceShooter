using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LevelSelectCanvasLogic : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI TMP_Chapter;
    [SerializeField] private Button BTN_Credits;
    [SerializeField] private Button BTN_ChapterSelect;

    private void OnDestroy()
    {
        SettingsManager.OnSettingsOpened -= OnSettingsOpenedHandler;
        SettingsManager.OnSettingsClosed -= OnSettingsClosedHandler;
        BTN_ChapterSelect.onClick.RemoveListener(OnChapterSelectButtonClickedHandler);
        BTN_Credits.onClick.RemoveListener(OnCreditsButtonClickedHandler);
    }
    private void Awake()
    {
        SettingsManager.OnSettingsOpened += OnSettingsOpenedHandler;
        SettingsManager.OnSettingsClosed += OnSettingsClosedHandler;
        BTN_ChapterSelect.onClick.AddListener(OnChapterSelectButtonClickedHandler);
        BTN_Credits.onClick.AddListener(OnCreditsButtonClickedHandler);

        TMP_Chapter.text = $"Chapter {GameDataSystem.currentChapter}";
    }

    private void OnChapterSelectButtonClickedHandler()
    {
        SceneSystem.Instance.LoadScene(Scenes.ChapterSelect);
    }
    private void OnCreditsButtonClickedHandler()
    {
        SceneSystem.Instance.LoadScene(Scenes.Credits);
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
