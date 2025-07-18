using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class MainMenuCanvasLogic : MonoBehaviour
{
    const string _logTag = "MainMenuCanvasLogic";

    [Header("Header")]
    [SerializeField] MainMenuCanvasHeaderLogic _header;

    [Header("Body")]
    [SerializeField] MainMenuCanvasBodyLogic _body;

    [Header("Game Save")]
    [SerializeField] GameObject _newSaveOverlay;
    [SerializeField] TMP_InputField _newSaveNameInputField;
    [SerializeField] TextMeshProUGUI _newSaveNameCharacterCount;

    [Header("Confirm Delete Save")]
    [SerializeField] GameObject _deleteSaveOverlay;
    [SerializeField] TextMeshProUGUI _deleteSaveNameText;
    int gameSaveToDelete = 0; // 1 = Save1, 2 = Save2, 3 = Save3


    int maxNewSaveNameCount = 7;
    int currentNewSaveNameCount = 0;

    Color newSaveNameCharacterCountBelowMaxColor = new Color(1f, 1f, 1f, .25f);
    Color newSaveNameCharacterCountMaxColor = new Color(1f, 0f, 0f, .25f);

    string newSaveName = string.Empty;


    public static event Action OnOpenSettings;
    private void Awake()
    {
        _header.Initialize();
        _body.Initialize();
        _newSaveNameCharacterCount.text = $"{currentNewSaveNameCount}/{maxNewSaveNameCount}";
    }

    public void Continue()
    {
        _header.Continue(); // This calls continue on _header after animation complete
    }

    public void OnSettingsButtonClickHandler()
    {
        OnOpenSettings?.Invoke();
    }

    // TODO: Make a separate class for the new game save object and make prefab
    public void OnDeleteLeftSaveButtonClickHandler()
    {
        _deleteSaveOverlay.SetActive(true);
        _deleteSaveNameText.text = SaveSystem.Instance.playerData.Save1Name;
        gameSaveToDelete = 1;
    }
    public void OnDeleteCenterSaveButtonClickHandler()
    {
        _deleteSaveOverlay.SetActive(true);
        _deleteSaveNameText.text = SaveSystem.Instance.playerData.Save2Name;
        gameSaveToDelete = 2;
    }
    public void OnDeleteRightSaveButtonClickHandler()
    {
        _deleteSaveOverlay.SetActive(true);
        _deleteSaveNameText.text = SaveSystem.Instance.playerData.Save3Name;
        gameSaveToDelete = 3;
    }




    public void OnCancelDeleteSaveButtonClickHandler()
    {
        _deleteSaveOverlay.SetActive(false);
    }
    public void OnConfirmDeleteSaveButtonClickHandler()
    {
        switch (gameSaveToDelete)
        {
            case 1:
                SaveSystem.Instance.ResetSave(SaveSystem.SaveIndex.Save1);
                break;
            case 2:
                SaveSystem.Instance.ResetSave(SaveSystem.SaveIndex.Save2);
                break;
            case 3:
                SaveSystem.Instance.ResetSave(SaveSystem.SaveIndex.Save3);
                break;
            default:
                LogSystem.Instance.Log("No valid save slot selected.", LogType.Error, _logTag);
                return;
        }
        SaveSystem.Instance.Save();
        gameSaveToDelete = 0;
        _body.UpdateGameSaves();
        _deleteSaveOverlay.SetActive(false);
    }

    public void SetActive(bool active)
    {
        if(active)
        {
            _header.gameObject.SetActive(true);
            _body.gameObject.SetActive(true);
        }
        else
        {
            _header.gameObject.SetActive(false);
            _body.gameObject.SetActive(false);
        }
    }

    public void UpdateNewSaveNameCharacterCountText()
    {
        currentNewSaveNameCount = _newSaveNameInputField.text.Length;
        _newSaveNameCharacterCount.text = $"{currentNewSaveNameCount}/{maxNewSaveNameCount}";
        if (currentNewSaveNameCount >= maxNewSaveNameCount)
        {
            _newSaveNameInputField.text = _newSaveNameInputField.text.Substring(0, maxNewSaveNameCount);
            currentNewSaveNameCount = maxNewSaveNameCount;
        }

        _newSaveNameCharacterCount.color = currentNewSaveNameCount >= maxNewSaveNameCount ? newSaveNameCharacterCountMaxColor : newSaveNameCharacterCountBelowMaxColor;
    }

    public void LoadGameButtonLeftHandler()
    {
        SaveSystem.Instance.currentGameSave = 1;
        if(SaveSystem.Instance.playerData.Save1Active)
        {
            AudioSystem.Instance.StopMusic();
            SceneSystem.Instance.LoadScene(Scenes.ChapterSelect);
        }
        else { DisplayCreateNewSaveOverlay(); }
    }
    public void LoadGameButtonCenterHandler()
    {
        SaveSystem.Instance.currentGameSave = 2;
        if (SaveSystem.Instance.playerData.Save2Active)
        {
            AudioSystem.Instance.StopMusic();
            SceneSystem.Instance.LoadScene(Scenes.ChapterSelect);
        } else { DisplayCreateNewSaveOverlay(); }
    }
    public void LoadGameButtonRightHandler()
    {
        SaveSystem.Instance.currentGameSave = 3;
        if (SaveSystem.Instance.playerData.Save3Active)
        {
            AudioSystem.Instance.StopMusic();
            SceneSystem.Instance.LoadScene(Scenes.ChapterSelect);
        }
        else { DisplayCreateNewSaveOverlay(); }
    }

    private void DisplayCreateNewSaveOverlay()
    {
        _body.gameObject.SetActive(false);
        _header.gameObject.SetActive(false);
        _newSaveOverlay.SetActive(true);
    }

    public void CreateNewSaveHandler()
    {
        newSaveName = _newSaveNameInputField.text.Trim();
        if (string.IsNullOrEmpty(newSaveName))
        {
            newSaveName = DateTime.Now.ToString("yyyy-MM-dd");
        }

        LogSystem.Instance.Log($"Creating new save with name: {newSaveName}", LogType.Todo, _logTag);
        switch(SaveSystem.Instance.currentGameSave)
        {
            case 1:
                SaveSystem.Instance.playerData.Save1Active = true;
                SaveSystem.Instance.playerData.Save1Name = newSaveName;
                break;
            case 2:
                SaveSystem.Instance.playerData.Save2Active = true;
                SaveSystem.Instance.playerData.Save2Name = newSaveName;
                break;
            case 3:
                SaveSystem.Instance.playerData.Save3Active = true;
                SaveSystem.Instance.playerData.Save3Name = newSaveName;
                break;
            default:
                LogSystem.Instance.Log("No valid save slot selected.", LogType.Error, _logTag);
                return;
        }
        SaveSystem.Instance.Save();
        AudioSystem.Instance.StopMusic();
        SceneSystem.Instance.LoadScene(Scenes.Credits);
    }
}
