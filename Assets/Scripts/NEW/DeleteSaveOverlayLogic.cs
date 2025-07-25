using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class DeleteSaveOverlayLogic : MonoBehaviour
{
    static readonly string _logTag = "DeleteSaveOverlayLogic";

    [SerializeField] private TextMeshProUGUI TMP_Name;
    [SerializeField] private Button BTN_Delete;
    [SerializeField] private Button BTN_Cancel;


    private SaveSlotLogic saveSlot;

    private void OnDestroy()
    {
        SaveSlotLogic.OnDeleteSave -= DeleteSave;
        BTN_Delete.onClick.RemoveListener(OnDeleteButtonClickedHandler);
        BTN_Cancel.onClick.RemoveListener(OnCancelButtonClickedHandler);
    }
    private void Awake()
    {
        SaveSlotLogic.OnDeleteSave += DeleteSave;
        BTN_Delete.onClick.AddListener(OnDeleteButtonClickedHandler);
        BTN_Cancel.onClick.AddListener(OnCancelButtonClickedHandler);

        gameObject.SetActive(false);
    }

    public void DeleteSave(SaveSlotLogic saveSlot)
    {
        gameObject.SetActive(true);
        this.saveSlot = saveSlot;

        TMP_Name.text = $"\"{SaveSystem.Instance.Load(saveSlot.ID).Name}\"";
    }
    
    private void OnDeleteButtonClickedHandler()
    {
        LogSystem.Instance.Log("Deleting Save: " + saveSlot.name + $" ID:{saveSlot.ID}", LogType.Warning, _logTag);
        SaveSystem.Instance.DeleteSave(saveSlot.ID);
        saveSlot.LoadSave();
        gameObject.SetActive(false);
    }
    private void OnCancelButtonClickedHandler()
    {
        gameObject.SetActive(false);
    }
}
