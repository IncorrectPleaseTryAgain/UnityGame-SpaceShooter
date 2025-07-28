using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SaveSlotLogic : MonoBehaviour
{
    static readonly string _logTag = "SaveSlotLogic";

    [Tooltip("Left = 1, Center = 2, Right = 3")]
    [SerializeField] private int id;
    public int ID { get { return id; } private set { id = value; } }

    [SerializeField] private Image IMG_Save;
    [SerializeField] private Button BTN_Delete;
    [SerializeField] private TextMeshProUGUI TMP_Name;

    public bool saveActive { get; private set; } = false;

    [SerializeField]
    static readonly Color Color_SaveSlotInactive = new Color(.5f, .5f, .5f, 0.25f);
    [SerializeField]
    static readonly Color Color_SaveSlotActive = new Color(1f, 1f, 1f, 1f);
    [SerializeField]
    static readonly Color Color_Delete = new Color(1f, 0f, 0f, 1f);

    [SerializeField]
    static readonly string NO_SAVE_NAME = "Empty";

    private void Awake()
    {
        BTN_Delete.gameObject.SetActive(false);
    }
    public void LoadSave()
    {
        LogSystem.Instance.Log($"Loading Save Slot {id}...", LogType.Info, _logTag);

        IMG_Save.color = Color_SaveSlotInactive;
        TMP_Name.text = NO_SAVE_NAME;
        TMP_Name.color = Color_SaveSlotInactive;
        BTN_Delete.image.color = Color_Delete;
        saveActive = false;

        // Save exists
        if (SaveSystem.Instance.SaveExists(id))
        {
            LogSystem.Instance.Log("Save Exists...");
            IMG_Save.color = Color_SaveSlotActive;
            TMP_Name.text = SaveSystem.Instance.Load(id).Name;
            TMP_Name.color = Color_SaveSlotActive;
            saveActive = true;
        }
    }

    public void OnSavePointerEnterHandler()
    {
        if (saveActive)
        {
            BTN_Delete.gameObject.SetActive(true);
        }
    }
    public void OnSavePointerExitHandler()
    {
        if (BTN_Delete.gameObject.activeSelf)
        {
            BTN_Delete.gameObject.SetActive(false);
        }
    }

    public static event Action<SaveSlotLogic> OnDeleteSave;
    public void OnDeleteButtonClickedHandler()
    {
        Debug.Log("Delete Save");
        OnDeleteSave?.Invoke(this);
    }

    public static event Action<int> OnEmptySaveSlotClicked;
    public void OnSaveButtonClicked()
    {
        if (saveActive)
        {
            SaveSystem.Instance.Load(id);
            SceneSystem.Instance.LoadScene(Scenes.ChapterSelect); // TODO: Change to level select
            return;
        }

        Debug.Log("Create New Save");
        OnEmptySaveSlotClicked?.Invoke(id);
    }
}
