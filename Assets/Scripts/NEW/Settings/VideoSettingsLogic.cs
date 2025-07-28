using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class VideoSettingsLogic : MonoBehaviour
{
    [Header("Sliders")]
    [SerializeField] private TMP_Dropdown DD_Resolution;
    [SerializeField] private Toggle TGL_Fullscreen;

    [SerializeField] private Button BTN_Reset;

    private void OnDestroy()
    {
        DD_Resolution.onValueChanged.RemoveListener(OnResolutionDropdownChangedHandler);
        TGL_Fullscreen.onValueChanged.RemoveListener(OnFullscreenToggleChangedHandler);
    }
    private void Awake()
    {
        DD_Resolution.onValueChanged.AddListener(OnResolutionDropdownChangedHandler);
        TGL_Fullscreen.onValueChanged.AddListener(OnFullscreenToggleChangedHandler);
        BTN_Reset.onClick.AddListener(OnResetHandler);

        // Load Resolution Options
        DD_Resolution.ClearOptions();
        DD_Resolution.AddOptions(VideoSystem.Instance.GetResolutionOptions());

        Load();
    }

    private void OnResolutionDropdownChangedHandler(int index)
    {
        VideoSystem.Instance.SetResolution(index);
    }
    private void OnFullscreenToggleChangedHandler(bool value)
    {
        VideoSystem.Instance.SetFullscreen(value);
    }

    private void OnResetHandler()
    {
        VideoSystem.Instance.Reset();
        TGL_Fullscreen.isOn = VideoSystem.Instance.defaultFullscreen;
        DD_Resolution.value = GetResolutionDropdownIndex(VideoSystem.Instance.defaultResolutionWidth, VideoSystem.Instance.defaultResolutionHeight);
        Load();
    }
    private int GetResolutionDropdownIndex(int width, int height)
    {
        return DD_Resolution.options.FindIndex(option => option.text == $"{width} x {height}");
    }
    public void Cancel()
    {
        VideoSystem.Instance.InitializeVideo();
    }
    public void Load()
    {
        TGL_Fullscreen.isOn = VideoSystem.Instance.fullscreen;
        DD_Resolution.value = GetResolutionDropdownIndex(VideoSystem.Instance.resolutionWidth, VideoSystem.Instance.resolutionHeight);
    }
    public void Save()
    {
        VideoSystem.Instance.SaveVideoSettings();
    }
}
