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
    [SerializeField] CreateNewSaveOverlayLogic _createNewSave;

    private void OnDestroy()
    {
        SaveSlotLogic.OnEmptySaveSlotClicked -= OnEmptySaveSlotClickedHandler;
        CreateNewSaveOverlayLogic.OnCreateNewSaveCancelButtonClicked -= SetActiveTrue;
        SettingsManager.OnSettingsClosed -= SetActiveTrue;
    }

    //public static event Action OnOpenSettings;
    private void Awake()
    {
        SaveSlotLogic.OnEmptySaveSlotClicked += OnEmptySaveSlotClickedHandler;
        CreateNewSaveOverlayLogic.OnCreateNewSaveCancelButtonClicked += SetActiveTrue;
        SettingsManager.OnSettingsClosed += SetActiveTrue;
        _header.Initialize();
        _body.Initialize();
        _createNewSave.Initialize();
        
    }

    public void Continue()
    {
        _header.Continue(); // This calls continue on _header after animation complete
    }

    private void SetActiveTrue()
    {
        _header.gameObject.SetActive(true);
        _body.gameObject.SetActive(true);
    }

    public void OnSettingsButtonClickHandler()
    {
        _header.gameObject.SetActive(false);
        _body.gameObject.SetActive(false);
        //OnOpenSettings?.Invoke();
    }

    private void OnEmptySaveSlotClickedHandler(int saveSlotID)
    {
        _header.gameObject.SetActive(false);
        _body.gameObject.SetActive(false);
    }
}
