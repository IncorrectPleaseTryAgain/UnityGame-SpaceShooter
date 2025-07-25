using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SaveSlotLogic : MonoBehaviour
{
    static readonly string _logTag = "SaveSlotLogic";

    [Tooltip("Left = 1, Center = 2, Right = 3")]
    [SerializeField] int saveSlotID;
    [SerializeField] Button BTN_Save;
    [SerializeField] Button BTN_Delete;
    [SerializeField] TextMeshProUGUI TMP_Name;

    public bool saveActive { get; private set; } = false;

    static readonly Color Color_Transparent = new Color(1f, 1f, 1f, 0f);
    [SerializeField]
    static readonly Color Color_SaveSlotActive = new Color(1f, 1f, 1f, 0.25f);
    [SerializeField]
    static readonly Color Color_SaveSlotInActive = new Color(1f, 1f, 1f, 1f);
    [SerializeField]
    static readonly Color Color_Delete = new Color(1f, 0f, 0f, 1f);

    [SerializeField]
    static readonly string NO_SAVE_NAME = "Empty";

    private void Awake()
    {
        Initialize();
    }
    private void Initialize()
    {
        LogSystem.Instance.Log($"Initializing Save Slot {saveSlotID}...", LogType.Info, _logTag);

        BTN_Save.image.color = Color_SaveSlotInActive;
        TMP_Name.text = NO_SAVE_NAME;
        BTN_Delete.image.color = Color_Delete;
        saveActive = false;

        // Save exists
        if (SaveSystem.Instance.SaveExists(saveSlotID))
        {
            BTN_Save.image.color = Color_SaveSlotActive;
            TMP_Name.text = SaveSystem.Instance.Load(saveSlotID).Name;
            saveActive = true;
        }

        BTN_Delete.gameObject.SetActive(false);
    }

    public void OnSaveButtonPointerEnterHandler()
    {
        if (saveActive)
        {
            BTN_Delete.gameObject.SetActive(true);
        }
    }
    public void OnSaveButtonPointerExitHandler()
    {
        if (saveActive)
        {
            BTN_Delete.gameObject.SetActive(true);
        }
    }

    public static event Action<int> OnEmptySaveSlotClicked;
    public void OnSaveButtonClicked()
    {
        if (saveActive)
        {
            SaveSystem.Instance.Load(saveSlotID);
            SceneSystem.Instance.LoadScene(Scenes.ChapterSelect); // TODO: Change to level select
        }

        OnEmptySaveSlotClicked?.Invoke(saveSlotID);
    }
}
