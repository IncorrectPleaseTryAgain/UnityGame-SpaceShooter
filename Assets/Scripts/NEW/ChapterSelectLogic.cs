using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ChapterSelectLogic : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI TMP_Name;
    [SerializeField] private Image IMG_Chapter;
    [SerializeField] private int id;

    private readonly Color Color_ChapterUnlocked = Color.white;
    private readonly Color Color_ChapterLocked = new Color(.5f, .5f, .5f,.25f);

    private bool isUnlocked = true;

    private void Awake()
    {
        TMP_Name.text = $"???";
        TMP_Name.color = Color_ChapterLocked;
        IMG_Chapter.color = Color_ChapterLocked;

        const int UNLOCKED_STATE = 1;
        isUnlocked = (GameDataSystem.currentSave.ChaptersUnlocked[id - 1] == UNLOCKED_STATE);

        if (isUnlocked)
        {
            TMP_Name.text = $"Chapter {id}";
            TMP_Name.color = Color_ChapterUnlocked;
            IMG_Chapter.color = Color_ChapterUnlocked;
        }
    }

    public void OnChapterClickHandler()
    {
        if (isUnlocked)
        {
            GameDataSystem.currentChapter = id;
            SceneSystem.Instance.LoadScene(Scenes.LevelSelect);
        }
    }
}
