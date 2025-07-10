using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;

public class VideoSystem : Singleton<VideoSystem>, ISystem
{
    const string _logTag = "VideoSystem";

    // Default Video Settings
    public int defaultResolutionWidth { get; private set; } = 1920;
    public int defaultResolutionHeight { get; private set; } = 1080;
    public bool defaultFullscreen { get; private set; } = true;

    // Current Video Settings
    public int resolutionWidth { get; private set; } = 1920;
    public int resolutionHeight { get; private set; } = 1080;
    public bool fullscreen { get; private set; } = true;

    public List<string> resolutionOptions { get; private set; } = new List<string>();

    public static event Action OnSystemInitialized;

    // Initialize System
    public IEnumerator Initialize()
    {
        LogSystem.Instance.Log("Initializing VideoSystem", LogType.Info, _logTag);

        if(VideoSystem.Instance == null) { yield return null; }

        // Initialize resolution options
        var resolutions = Screen.resolutions;
        foreach (var res in resolutions)
        {
            resolutionOptions.Add($"{res.width} x {res.height}");
        }

        OnSystemInitialized?.Invoke();
    }
    // Initialize Video
    public void InitializeVideo()
    {
        fullscreen = SaveSystem.Instance.GetFullscreen(defaultFullscreen ? 1 : 0);
        resolutionWidth = SaveSystem.Instance.GetResolutionWidth(defaultResolutionWidth);
        resolutionHeight = SaveSystem.Instance.GetResolutionHeight(defaultResolutionHeight);
        Screen.SetResolution(resolutionWidth, resolutionHeight, fullscreen);

        LogSystem.Instance.Log($"Video Settings Initialized: {resolutionWidth} x {resolutionHeight}, Fullscreen: {fullscreen}", LogType.Info, _logTag);
    }
    public void Reset()
    {
        fullscreen = defaultFullscreen;
        resolutionWidth = defaultResolutionWidth;
        resolutionHeight = defaultResolutionHeight;
        Screen.SetResolution(defaultResolutionWidth, defaultResolutionHeight, defaultFullscreen);
    }

    /*
     * Video Methods
     */
    // Getters
    private int GetHeightResolutionOption(int index)
    {
        string res = resolutionOptions[index];
        // Get first integer in string

        Match match = Regex.Match(res, @"\d+");
        if (match.Success)
        {
            if (int.TryParse(match.Value, out int result))
            {
                return result;
            }
        }
        return 0;
    }
    private int GetWidthResolutionOption(int index)
    {
        string res = resolutionOptions[index];
        // Get second integer in string

        MatchCollection matches = Regex.Matches(res, @"\d+");

        if (matches.Count >= 2)
        {
            if (int.TryParse(matches[1].Value, out int result))
            {
                return result;
            }
        }

        return 0;
    }
    public List<string> GetResolutionOptions()
    {
        return resolutionOptions;
    }
    public void SetFullscreen(bool isFullscreen)
    {
        fullscreen = isFullscreen;
        Screen.fullScreen = fullscreen;
    }
    // Setters
    public void SetResolution(int dropdownIndex)
    {
        resolutionWidth = GetWidthResolutionOption(dropdownIndex);
        resolutionHeight = GetHeightResolutionOption(dropdownIndex);
        Screen.SetResolution(resolutionWidth, resolutionHeight, Screen.fullScreen);
    }
    /*
     * Save
     */
    public void SaveVideoSettings()
    {
        SaveSystem.Instance.SaveVideoSettings(resolutionWidth, resolutionHeight, fullscreen ? 1 : 0);

        LogSystem.Instance.Log($"Video Settings Saved: {resolutionWidth} x {resolutionHeight}, Fullscreen: {fullscreen}", LogType.Info, _logTag);
    }

}