using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using System;

public class SettingsManager : MonoBehaviour
{
    static readonly string _logTag = "SettingsManager";

    [SerializeField] private GameObject settingsOverlay;
    [SerializeField] private AudioSettingsLogic _audio;
    [SerializeField] private VideoSettingsLogic _video;
    [SerializeField] private ControlsSettingsLogic _controls;

    [SerializeField] private Button BTN_Settings;
    [SerializeField] private Button BTN_Cancel;
    [SerializeField] private Button BTN_Save;

    public static event Action OnSettingsOpened;
    public static event Action OnSettingsClosed;
    private void OnDestroy()
    {
        BTN_Settings.onClick.RemoveListener(OnSettingsButtonClickedHandler);
        BTN_Cancel.onClick.RemoveListener(OnCancelButtonClickedHandler);
        BTN_Save.onClick.RemoveListener(OnSaveButtonClickedHandler);
    }
    private void Awake()
    {
        BTN_Settings.onClick.AddListener(OnSettingsButtonClickedHandler);
        BTN_Cancel.onClick.AddListener(OnCancelButtonClickedHandler);
        BTN_Save.onClick.AddListener(OnSaveButtonClickedHandler);

        _audio.Load();
        _video.Load();
        _controls.Load();

        settingsOverlay.gameObject.SetActive(false);
    }
    private void OnSettingsButtonClickedHandler()
    {
        if (settingsOverlay.activeSelf) { OnCancelButtonClickedHandler(); return; }
        LogSystem.Instance.Log("Opening settings...", LogType.Info, _logTag);

        _audio.Load();
        _video.Load();
        _controls.Load();

        settingsOverlay.gameObject.SetActive(true);
        OnSettingsOpened?.Invoke();
    }
    private void OnCancelButtonClickedHandler()
    {
        LogSystem.Instance.Log("Closing settings...", LogType.Info, _logTag);

        _audio.Cancel();
        _video.Cancel();
        _controls.Load();

        settingsOverlay.gameObject.SetActive(false);
        OnSettingsClosed?.Invoke();
    }
    private void OnSaveButtonClickedHandler()
    {
        LogSystem.Instance.Log("Saving settings...", LogType.Info, _logTag);

        _audio.Save();
        _video.Save();
        _controls.Save();

        OnSettingsClosed?.Invoke();
        settingsOverlay.gameObject.SetActive(false);
    }
}
