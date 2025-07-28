using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LevelSelectLogic : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI TMP_Name;
    [SerializeField] private Image IMG_Level;
    [SerializeField] private int id;

    private readonly Color Color_LevelUnlocked = Color.white;
    private readonly Color Color_LevelLocked = new Color(.5f, .5f, .5f, .25f);

    private bool isUnlocked = true;

    private void Awake()
    {
        TMP_Name.text = $"???";
        TMP_Name.color = Color_LevelLocked;
        IMG_Level.color = Color_LevelLocked;

        const int UNLOCKED_STATE = 1;
        isUnlocked = (GameDataSystem.currentSave.LevelsUnlocked[id - 1] == UNLOCKED_STATE);

        // If the next chapter is unlocked then all the levels in this chapter are automatically unlocked
        const int FINAL_CHAPTER = 5;
        if((GameDataSystem.currentChapter != FINAL_CHAPTER) && (GameDataSystem.currentSave.ChaptersUnlocked[GameDataSystem.currentChapter] == UNLOCKED_STATE))
        {
            isUnlocked = true;
        }

        if (isUnlocked)
        {
            TMP_Name.text = $"Level {id}";
            TMP_Name.color = Color_LevelUnlocked;
            IMG_Level.color = Color_LevelUnlocked;
        }
    }

    public void OnLevelClickHandler()
    {
        if (isUnlocked)
        {
            GameDataSystem.currentLevel = id;
            SceneSystem.Instance.LoadScene(Scenes.InGame);
        }
    }
}
