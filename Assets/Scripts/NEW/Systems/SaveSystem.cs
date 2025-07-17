using System;
using System.Collections;
using System.IO;
using UnityEngine;
using UnityEngine.InputSystem;


public class SaveSystem : Singleton<SaveSystem>, ISystem
{
    [SerializeField] InputActionAsset actions;

    public enum SaveIndex
    {
        NOSAVE = 0,
        Save1 = 1,
        Save2 = 2,
        Save3 = 3
    }

    struct PlayerPrefKeys
    {
        // Audio
        public const string MASTER_VOLUME_KEY = "master_volume_key";
        public const string MUSIC_VOLUME_KEY = "music_volume_key";
        public const string SFX_VOLUME_KEY = "sfx_volume_key";

        // Video
        public const string RESOLUTION_WIDTH_KEY = "res_width_key";
        public const string RESOLUTION_HEIGHT_KEY = "res_height_key";
        public const string FULLSCREEN_KEY = "fullscreen_key";

        // Controls
        public const string CONTROLS_KEY = "controls_key";
    }

    const string _logTag = "SaveSystem";

    public PlayerData playerData;
    public int currentGameSave = 0;

    const string _saveFileName = "/playerData.json";
    string _saveFilePath;

    public static event Action OnSystemInitialized;
    // Initialize System
    public IEnumerator Initialize()
    {
        LogSystem.Instance.Log("Initializing SaveSystem...", LogType.Info, _logTag);

        if (SaveSystem.Instance == null) { yield return null; }

        _saveFilePath = Application.persistentDataPath + _saveFileName;
        Load();

        OnSystemInitialized?.Invoke();
    }

    /*
     * Json Data Structure
     */
    /*
     * Player Data
     */
    public void Save()
    {
        LogSystem.Instance.Log("Saving player data to file path: " + _saveFilePath, LogType.Info, _logTag);

        try
        {
            string json = JsonUtility.ToJson(playerData, true);
            File.WriteAllText(_saveFilePath, json);
            LogSystem.Instance.Log("Player data saved successfully.", LogType.Info, _logTag);
        }
        catch (Exception e)
        {
            LogSystem.Instance.Log($"Error saving player data: {e.Message}", LogType.Error, _logTag);
        }

        PlayerPrefs.Save();
    }
    public PlayerData Load()
    {
        LogSystem.Instance.Log("Loading player data from file.", LogType.Info, _logTag);

        try
        {
            string json = File.ReadAllText(_saveFilePath);
            playerData = JsonUtility.FromJson<PlayerData>(json);
            LogSystem.Instance.Log("Player data loaded successfully.", LogType.Info, _logTag);
        }
        catch (Exception e)
        {
            LogSystem.Instance.Log($"Error loading player data: {e.Message}", LogType.Error, _logTag);
            playerData = new PlayerData(); // Reset to default if loading fails
            Save();
            Load();
        }

        return playerData;
    }

    public void ResetSave(SaveIndex save)
    {
        switch (save)
        {
            case SaveIndex.Save1:
                LogSystem.Instance.Log("Resetting Save 1", LogType.Info, _logTag);
                playerData.Save1Name = "Create Save";
                playerData.Save1Active = false;
                playerData.Save1Chapter = 1;
                playerData.Save1Level = 1;
                break;
            case SaveIndex.Save2:
                LogSystem.Instance.Log("Resetting Save 2", LogType.Info, _logTag);
                playerData.Save2Name = "Create Save";
                playerData.Save2Active = false;
                playerData.Save2Chapter = 1;
                playerData.Save2Level = 1;
                break;
            case SaveIndex.Save3:
                LogSystem.Instance.Log("Resetting Save 3", LogType.Info, _logTag);
                playerData.Save3Name = "Create Save";
                playerData.Save3Active = false;
                playerData.Save3Chapter = 1;
                playerData.Save3Level = 1;
                break;
        }
    }

    /*
     * PlayerPrefs Data Structure
     */
    /*
     * Audio Data
     */
    public float GetMasterVolume(float defaultVolume)
    {
        return PlayerPrefs.GetFloat(PlayerPrefKeys.MASTER_VOLUME_KEY, defaultVolume);
    }
    public float GetMusicVolume(float defaultVolume)
    {
        return PlayerPrefs.GetFloat(PlayerPrefKeys.MUSIC_VOLUME_KEY, defaultVolume);
    }
    public float GetSfxVolume(float defaultVolume)
    {
        return PlayerPrefs.GetFloat(PlayerPrefKeys.SFX_VOLUME_KEY, defaultVolume);
    }
    public void SaveAudioSettings(float masterVolume, float musicVolume, float sfxVolume)
    {
        PlayerPrefs.SetFloat(PlayerPrefKeys.MASTER_VOLUME_KEY , masterVolume);
        PlayerPrefs.SetFloat(PlayerPrefKeys.MUSIC_VOLUME_KEY, musicVolume);
        PlayerPrefs.SetFloat(PlayerPrefKeys.SFX_VOLUME_KEY, sfxVolume);
        PlayerPrefs.Save();
    }

    /*
    * Video Data
    */
    public int GetResolutionWidth(int defaultResWidth)
    {
        return PlayerPrefs.GetInt(PlayerPrefKeys.RESOLUTION_WIDTH_KEY, defaultResWidth);
    }
    public int GetResolutionHeight(int defaultResHeight)
    {
        return PlayerPrefs.GetInt(PlayerPrefKeys.RESOLUTION_HEIGHT_KEY, defaultResHeight);
    }
    public bool GetFullscreen(int defaultIsFullscreen)
    {
        return PlayerPrefs.GetInt(PlayerPrefKeys.FULLSCREEN_KEY, defaultIsFullscreen) == 1;
    }
    public void SaveVideoSettings(int resWidth, int resHeight, int fullscreen)
    {
        LogSystem.Instance.Log($"Saving video settings: {resWidth} x {resHeight}, Fullscreen: {fullscreen == 1}", LogType.Info, _logTag);

        PlayerPrefs.SetInt(PlayerPrefKeys.RESOLUTION_WIDTH_KEY, resWidth);
        PlayerPrefs.SetInt(PlayerPrefKeys.RESOLUTION_HEIGHT_KEY, resHeight);
        PlayerPrefs.SetInt(PlayerPrefKeys.FULLSCREEN_KEY, fullscreen);
        PlayerPrefs.Save();
    }

    /*
     * Controls Data
     */
    public void LoadControls()
    {
        LogSystem.Instance.Log("Loading control bindings from PlayerPrefs.", LogType.Debug, _logTag);
        string rebinds = PlayerPrefs.GetString(PlayerPrefKeys.CONTROLS_KEY);
        if (!string.IsNullOrEmpty(rebinds))
            actions.LoadBindingOverridesFromJson(rebinds);
    }

    public void SaveControls()
    {
        LogSystem.Instance.Log("Saving control bindings to PlayerPrefs.", LogType.Debug, _logTag);
        string rebinds = actions.SaveBindingOverridesAsJson();
        PlayerPrefs.SetString(PlayerPrefKeys.CONTROLS_KEY, rebinds);
        PlayerPrefs.Save();
    }
}

