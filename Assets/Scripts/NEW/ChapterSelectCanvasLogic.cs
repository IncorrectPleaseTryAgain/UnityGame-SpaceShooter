using TMPro;
using UnityEngine.UI;
using UnityEngine;
using System;

public class ChapterSelectCanvasLogic : MonoBehaviour
{
    [SerializeField] Button[] chapterButtons;
    [SerializeField] GameObject settings;
    public static event Action OnOpenSettings;


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

    public void OnMainMenuButtonClicked()
    {
        AudioSystem.Instance.StopMusic();   
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
