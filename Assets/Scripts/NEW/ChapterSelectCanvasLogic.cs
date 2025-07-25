using TMPro;
using UnityEngine.UI;
using UnityEngine;
using System;

public class ChapterSelectCanvasLogic : MonoBehaviour
{
    [SerializeField] Button[] chapterButtons;
    [SerializeField] GameObject settings;
    public static event Action OnOpenSettings;

    public void Initialize()
    {
        InitializeChapterButtons();
    }

    private void InitializeChapterButtons()
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
        //int numChaptersUnlocked = GameDataSystem.Instance.gameData.Save1Chapter;
        //for (int i = 0; i < numChaptersUnlocked; i++)
        //{
        //    if (i < numChaptersUnlocked)
        //    {
        //        SetLevelButtonActive(chapterButtons[i], true);
        //    }
        //    else
        //    {
        //        SetLevelButtonActive(chapterButtons[i], false);
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
        //int numChaptersUnlocked = GameDataSystem.Instance.gameData.Save2Chapter;
        //for (int i = 0; i < numChaptersUnlocked; i++)
        //{
        //    if (i < numChaptersUnlocked)
        //    {
        //        SetLevelButtonActive(chapterButtons[i], true);
        //    }
        //    else
        //    {
        //        SetLevelButtonActive(chapterButtons[i], false);
        //    }
        //}
    }

    private void InitializeButtonsForSave3()
    {
        //int numChaptersUnlocked = GameDataSystem.Instance.gameData.Save3Chapter;
        //for (int i = 0; i < numChaptersUnlocked; i++)
        //{
        //    if (i < numChaptersUnlocked)
        //    {
        //        SetLevelButtonActive(chapterButtons[i], true);
        //    }
        //    else
        //    {
        //        SetLevelButtonActive(chapterButtons[i], false);
        //    }
        //}
    }

    public void OnMainMenuButtonClicked()
    {
        SceneSystem.Instance.LoadScene(Scenes.MainMenu);
    }

    public void OnChapterButtonClicked(int chapterIndex)
    {
        //GameDataSystem.Instance.currentChapter = chapterIndex;
        SceneSystem.Instance.LoadScene(Scenes.LevelSelect);
    }

    public void OnSettingsButtonClickHandler()
    {
        gameObject.SetActive(false);
        OnOpenSettings?.Invoke();
    }
}
