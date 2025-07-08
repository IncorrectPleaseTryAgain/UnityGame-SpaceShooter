using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SettingsManager : MonoBehaviour
{
    const string _logTag = "SettingsManager";

    [Header("Audio")]
    [SerializeField] Slider _audioMasterSlider;
    [SerializeField] Slider _audioMusicSlider;
    [SerializeField] Slider _audioSFXSlider;

    [Header("Video")]
    [SerializeField] Toggle _fullscreenToggle;
    [SerializeField] TMP_Dropdown _resolutionDropdown;
    const string _fullscreenKey = "Fullscreen";
    const string _resolutionKey = "Resolution";

    [Header("Controls")]
    [SerializeField] Button _keybindForward;
    [SerializeField] Button _keybindBackward;
    [SerializeField] Button _keybindLeft;
    [SerializeField] Button _keybindRight;
    const string _keybindForwardKey = "fowards";
    const string _keybindBackwardKey = "backwards";
    const string _keybindLeftKey = "left";
    const string _keybindRightKey = "right";

    [SerializeField] Button _keybindAttack;
    [SerializeField] Button _keybindAbility;
    const string _keybindAttackKey = "attack";
    const string _keybindAbilityKey = "ability";

    bool audioSettingsChanged = false;
    bool videoSettingsChanged = false;
    bool controlsSettingsChanged = false;

    private void OnEnable()
    {
        ButtonLogic.OnButtonAction += HandleButtonAction;
    }

    private void OnDisable()
    {
        ButtonLogic.OnButtonAction -= HandleButtonAction;
    }

    private void OnDestroy()
    {
        TerminateListeners();
    }

    private void TerminateListeners()
    {
        // Audio
        _audioMasterSlider.onValueChanged.RemoveAllListeners();
        _audioMusicSlider.onValueChanged.RemoveAllListeners();
        _audioSFXSlider.onValueChanged.RemoveAllListeners();

        // Video
        _fullscreenToggle.onValueChanged.RemoveAllListeners();
        _resolutionDropdown.onValueChanged.RemoveAllListeners();

        // Controls
        _keybindForward.onClick.RemoveAllListeners();
        _keybindBackward.onClick.RemoveAllListeners();
        _keybindLeft.onClick.RemoveAllListeners();
        _keybindRight.onClick.RemoveAllListeners();
        _keybindAttack.onClick.RemoveAllListeners();
        _keybindAbility.onClick.RemoveAllListeners();
    }

    private void Start()
    {
        InitializeAudioSettings();
        InitializeVideoSettings();
        InitializeControlsSettings();
    }

    void HandleButtonAction(Actions action)
    {
        switch (action)
        {
            case Actions.OpenSettings:
                OpenSettingsHandler();
                break;
            case Actions.CloseSettings:
                CloseSettingsHandler();
                break;
            case Actions.SaveSettings:
                SaveSettingsHandler();
                break;
            case Actions.ResetSettingsAudio:
                ResetSettingsAudioHandler();
                break;
            case Actions.ResetSettingsControls:
                ResetSettingsControlsHandler();
                break;
            case Actions.ResetSettingsVideo:
                ResetSettingsVideoHandler();
                break;

        }
    }

    private void OpenSettingsHandler()
    {
        LogSystem.Instance.Log("Opening settings menu.", LogType.Todo, _logTag);
    }

    private void CloseSettingsHandler()
    {
        LogSystem.Instance.Log("Closing settings menu.", LogType.Todo, _logTag);
    }

    private void SaveSettingsHandler()
    {
        LogSystem.Instance.Log("Saving settings.", LogType.Todo, _logTag);
    }

    private void ResetSettingsAudioHandler()
    {
        LogSystem.Instance.Log("Resetting audio settings to default.", LogType.Todo, _logTag);
    }

    private void ResetSettingsControlsHandler()
    {
        LogSystem.Instance.Log("Resetting control settings to default.", LogType.Todo, _logTag);
    }

    private void ResetSettingsVideoHandler()
    {
        LogSystem.Instance.Log("Resetting video settings to default.", LogType.Todo, _logTag);
    }


    /*
     * Audio Settings Methods
     */
    void InitializeAudioSettings()
    {
        if (_audioMasterSlider)
        {
            _audioMasterSlider.value = AudioSystem.Instance.GetMixerMasterVolume();
            _audioMasterSlider.onValueChanged.AddListener(AudioMasterSliderValueChangedHandler);
        }
        else { LogSystem.Instance.Log("_audioMasterSlider null reference", LogType.Error, _logTag); }
        if (_audioMusicSlider)
        {
            _audioMusicSlider.value = AudioSystem.Instance.GetMixerMusicVolume();
            _audioMusicSlider.onValueChanged.AddListener(AudioMusicSliderValueChangedHandler);
        }
        else { LogSystem.Instance.Log("_audioMusicSlider null reference", LogType.Error, _logTag); }
        if (_audioSFXSlider)
        {
            _audioSFXSlider.value = AudioSystem.Instance.GetMixerSfxVolume();
            _audioSFXSlider.onValueChanged.AddListener(AudioSfxSliderValueChangedHandler);
        }
        else { LogSystem.Instance.Log("_audioSFXSlider null reference", LogType.Error, _logTag); }
    }
    void AudioMasterSliderValueChangedHandler(float value)
    {
        audioSettingsChanged = true;
        AudioSystem.Instance.SetMixerMasterVolume(value);
    }
    void AudioMusicSliderValueChangedHandler(float value)
    {
        audioSettingsChanged = true;
        AudioSystem.Instance.SetMixerMusicVolume(value);
    }
    void AudioSfxSliderValueChangedHandler(float value)
    {
        audioSettingsChanged = true;
        AudioSystem.Instance.SetMixerSfxVolume(value);
    }

    /*
     * Video Settings Methods
     */
    void InitializeVideoSettings()
    {
        if (_fullscreenToggle)
        {
            _fullscreenToggle.isOn = VideoSystem.Instance.GetIsFullscreen();
            _fullscreenToggle.onValueChanged.AddListener(FullscreeenToggleValueChangedHandler);
        }
        else { LogSystem.Instance.Log("_fullscreenToggle null reference", LogType.Error, _logTag); }
        if (_resolutionDropdown)
        {
            // Add resolution options
            _resolutionDropdown.ClearOptions();
            _resolutionDropdown.AddOptions(VideoSystem.Instance.GetResolutionOptions());

            // Set value
            _resolutionDropdown.value = _resolutionDropdown.options.FindIndex(option => option.text == $"{Screen.currentResolution.width} x {Screen.currentResolution.height}");
            _resolutionDropdown.onValueChanged.AddListener(ResolutionDropdownValueChangedHandler);
        }
        else { LogSystem.Instance.Log("_resolutionDropdown null reference", LogType.Error, _logTag); }
    }
    void FullscreeenToggleValueChangedHandler(bool value)
    {
        videoSettingsChanged = true;
        VideoSystem.Instance.SetIsFullscreen(value);
    }
    void ResolutionDropdownValueChangedHandler(int index)
    {
        videoSettingsChanged = true;
        VideoSystem.Instance.SetResolution(index);
    }


    /*
     * Controls Settings Methods
     */
    private void InitializeControlsSettings()
    {
        LogSystem.Instance.Log("Implement initialize controls settings", LogType.Todo, _logTag);
    }

    public void SaveSettings()
    {
        if(audioSettingsChanged)
        {
            audioSettingsChanged = false;
            AudioSystem.Instance.SaveAudioSettings();
        }
        if(videoSettingsChanged)
        {
            videoSettingsChanged = false;
            VideoSystem.Instance.SaveVideoSettings();
        }
        if(controlsSettingsChanged)
        {
            controlsSettingsChanged = false;

        }
    }
}
