using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LevelSelectCanvasLogic : MonoBehaviour
{
    [SerializeField] Button[] levelButtons;
    [SerializeField] TextMeshProUGUI chapterText;
    [SerializeField] GameObject settings;
    public static event Action OnOpenSettings;

    public void Initialize()
    {
        InitializeChapterText();
        InitializeLevelButtons();
    }


    private void InitializeChapterText()
    {
        //chapterText.text = $"Chapter {GameDataSystem.Instance.currentChapter}";
    }
    private void InitializeLevelButtons()
    {
        //switch (GameDataSystem.Instance.currentSave)
        //{
        //    case (int)SaveSystem.SaveIndex.Save1:
        //        InitializeButtonsForSave1();
        //        break;
        //    case (int)SaveSystem.SaveIndex.Save2:
        //        InitializeButtonsForSave2();
        //        break;
        //    case (int)SaveSystem.SaveIndex.Save3:
        //        InitializeButtonsForSave3();
        //        break;
        //    default:
        //        Debug.LogError("Invalid save index. Please check the currentGameSave value in SaveSystem.");
        //        return;
        //}
    }

    private void InitializeButtonsForSave1()
    {
        //if (GameDataSystem.Instance.currentChapter < GameDataSystem.Instance.gameData.Save1Chapter)
        //{
        //    foreach (Button button in levelButtons)
        //    {
        //        SetLevelButtonActive(button, true);
        //    }
        //}
        //else
        //{
        //    int numLevelsUnlocked = GameDataSystem.Instance.gameData.Save1Level;
        //    for (int i = 0; i < numLevelsUnlocked; i++)
        //    {
        //        if (i < numLevelsUnlocked)
        //        {
        //            SetLevelButtonActive(levelButtons[i], true);
        //        }
        //        else
        //        {
        //            SetLevelButtonActive(levelButtons[i], false);
        //        }
        //    }
        //}
    }

    private void SetLevelButtonActive(Button button, bool active)
    {
        if (active)
        {
            button.interactable = true;
            button.GetComponentInChildren<TextMeshProUGUI>().color = Color.white;
            button.GetComponent<Image>().color = Color.white;
        }
        else
        {
            button.interactable = false;
            button.GetComponentInChildren<TextMeshProUGUI>().color = new Color(1f, 1f, 1f, .2f);
            button.GetComponent<Image>().color = new Color(1f, 1f, 1f, .2f);
        }
    }

    private void InitializeButtonsForSave2()
    {
        //if (GameDataSystem.Instance.currentChapter < GameDataSystem.Instance.gameData.Save2Chapter)
        //{
        //    foreach (Button button in levelButtons)
        //    {
        //        SetLevelButtonActive(button, true);
        //    }
        //}
        //else
        //{
        //    int numLevelsUnlocked = GameDataSystem.Instance.gameData.Save2Level;
        //    for (int i = 0; i < numLevelsUnlocked; i++)
        //    {
        //        if (i < numLevelsUnlocked)
        //        {
        //            SetLevelButtonActive(levelButtons[i], true);
        //        }
        //        else
        //        {
        //            SetLevelButtonActive(levelButtons[i], false);
        //        }
        //    }
        //}
    }

    private void InitializeButtonsForSave3()
    {
        //if (GameDataSystem.Instance.currentChapter < GameDataSystem.Instance.gameData.Save3Chapter)
        //{
        //    foreach (Button button in levelButtons)
        //    {
        //        SetLevelButtonActive(button, true);
        //    }
        //}
        //else
        //{
        //    int numLevelsUnlocked = GameDataSystem.Instance.gameData.Save3Level;
        //    for (int i = 0; i < numLevelsUnlocked; i++)
        //    {
        //        if (i < numLevelsUnlocked)
        //        {
        //            SetLevelButtonActive(levelButtons[i], true);
        //        }
        //        else
        //        {
        //            SetLevelButtonActive(levelButtons[i], false);
        //        }
        //    }
        //}
    }
    public void OnChapterSelectButtonClicked()
    {
        SceneSystem.Instance.LoadScene(Scenes.ChapterSelect);
    }

    public void OnCreditsButtonClicked()
    {
        SceneSystem.Instance.LoadScene(Scenes.Credits);
    }

    public void OnSettingsButtonClickHandler()
    {
        gameObject.SetActive(false);
        OnOpenSettings?.Invoke();
    }

    public void OnLevelButtonCkicled(int level)
    {
        //GameDataSystem.Instance.currentLevel = level;
        SceneSystem.Instance.LoadScene(Scenes.InGame);
    }
}
