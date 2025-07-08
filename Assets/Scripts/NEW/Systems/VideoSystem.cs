using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;

public class VideoSystem : Singleton<VideoSystem>, ISystem
{
    const string _logTag = "VideoSystem";

    // Video Settings
    int resolutionWidth;
    int resolutionHeight;
    bool fullscreen;

    const int defaultResolutionWidth = 1920;
    const int defaultResolutionHeight = 1080;
    const int defaultFullscreen = 1;

    List<string> resolutionOptions = new List<string>();

    public static event Action OnSystemInitialized;

    public IEnumerator Initialize()
    {
        LogSystem.Instance.Log("Initializing VideoSystem", LogType.Info, _logTag);

        if(VideoSystem.Instance == null) { yield return null; }

        var resolutions = Screen.resolutions;
        foreach (var res in resolutions)
        {
            resolutionOptions.Add($"{res.width} x {res.height}");
        }
        resolutionWidth = SaveSystem.Instance.GetResolutionWidth(defaultResolutionWidth);
        resolutionHeight = SaveSystem.Instance.GetResolutionHeight(defaultResolutionHeight);

        fullscreen = SaveSystem.Instance.GetIsFullscreen(defaultFullscreen);

        Screen.SetResolution(resolutionWidth, resolutionHeight, fullscreen);

        OnSystemInitialized?.Invoke();
    }

    public List<string> GetResolutionOptions()
    {
        return resolutionOptions;
    }

    public void SetIsFullscreen(bool value)
    {
        fullscreen = value;
    }

    public bool GetIsFullscreen() {  return fullscreen; }

    public void SetResolution(int index)
    {
        int width = GetWidthResolutionOption(index);
        int height = GetHeightResolutionOption(index);
        if(width == 0|| height == 0)
        {
            LogSystem.Instance.Log("Failed getting width or height in SetResolution()", LogType.Error, _logTag);
            return;
        }
        Screen.SetResolution(width, height, fullscreen);
    }

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

    public void SaveVideoSettings()
    {
        SaveSystem.Instance.SaveVideoSettings(resolutionWidth, resolutionHeight, fullscreen ? 1 : 0);
    }

}