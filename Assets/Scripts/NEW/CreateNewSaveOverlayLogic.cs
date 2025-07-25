using System;
using TMPro;
using UnityEngine;

public class CreateNewSaveOverlayLogic : MonoBehaviour
{
    static readonly string _logTag = "CreateNewSaveOverlayLogic";

    [SerializeField] TMP_InputField INPT_Name;
    [SerializeField] TextMeshProUGUI TMP_CharacterCount;

    [SerializeField]
    Color Color_CharacterCountNormal = new Color(1f, 1f, 1f, .25f);
    [SerializeField] 
    Color Color_CharacterCountMax = new Color(1f, 0f, 0f, .5f);

    [SerializeField]
    static readonly int maxNameLength = 7;

    private SaveData newSave;

    private void OnDestroy()
    {
        SaveSlotLogic.OnEmptySaveSlotClicked -= CreateNewSave;
    }
    private void Awake()
    {
        SaveSlotLogic.OnEmptySaveSlotClicked += CreateNewSave;
        TMP_CharacterCount.text = $"0/{maxNameLength}";

        gameObject.SetActive(false);
    }

    public void OnInputFieldUpdate()
    {
        TMP_CharacterCount.text = $"{INPT_Name.text.Length}/{maxNameLength}";
        TMP_CharacterCount.color = Color_CharacterCountNormal;

        if (INPT_Name.text.Length >= maxNameLength)
        {
            INPT_Name.text = INPT_Name.text.Substring(0, maxNameLength);
            TMP_CharacterCount.color = Color_CharacterCountMax;
        }
    }

    private void CreateNewSave(int saveSlotID)
    {
        gameObject.SetActive(true);
        newSave = new SaveData();
        newSave.saveSlotID = saveSlotID;
    }

    public void OnCreateNewSaveButtonClicked()
    {
        string newSaveName = INPT_Name.text.Trim();
        if (string.IsNullOrEmpty(newSaveName))
        {
            newSaveName = DateTime.Now.ToString("yyyy-MM-dd");
        }

        LogSystem.Instance.Log($"Creating new save with name: {newSaveName}", LogType.Todo, _logTag);

        newSave.Name = newSaveName;
        newSave.DateCreated = DateTime.Now.ToString("yyyy-MM-dd");

        SaveSystem.Instance.Save(newSave, newSave.saveSlotID);
        SceneSystem.Instance.LoadScene(Scenes.Credits);
    }

    public void OnCancelButtonClicked()
    {
        INPT_Name.text = string.Empty;
        gameObject.SetActive(false);
    }
}
