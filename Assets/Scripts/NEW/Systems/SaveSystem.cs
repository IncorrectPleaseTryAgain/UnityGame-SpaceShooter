using System;
using System.Collections;
using System.IO;
using UnityEngine;
using UnityEngine.InputSystem;


public class SaveSystem : Singleton<SaveSystem>, ISystem
{
    static readonly string _logTag = "SaveSystem";
    [SerializeField] private InputActionAsset inputActionAsset;

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


    const string SAVE_FOLDER_NAME = "/saves/";
    string saveFolderPath;

    public static event Action OnSystemInitialized;
    // Initialize System
    public IEnumerator Initialize()
    {
        LogSystem.Instance.Log("Initializing SaveSystem...", LogType.Info, _logTag);

        if (SaveSystem.Instance == null) { yield return null; }

        saveFolderPath = Application.persistentDataPath + SAVE_FOLDER_NAME;

        OnSystemInitialized?.Invoke();
    }

    /*
     * Json Data Structure
     */
    /*
     * Player Data
     */
    public void Save(SaveData saveData, int slot)
    {
        if(slot < 0 || slot > 3) { throw new ArgumentException($"Invalid Saving Slot {{slot}}"); }
        LogSystem.Instance.Log("Saving...", LogType.Debug, _logTag);

        try
        {
            if (!Directory.Exists(saveFolderPath)) { Directory.CreateDirectory(saveFolderPath); }

            string path = GetPath(slot);
            string json = JsonUtility.ToJson(saveData, true);
            File.WriteAllText(path, json);

            LogSystem.Instance.Log($"Saved Successfully. {path}", LogType.Debug, _logTag);
        }
        catch (Exception e)
        {
            LogSystem.Instance.Log($"Error: {e.Message}", LogType.Error, _logTag);
        }

        PlayerPrefs.Save();
    }
    public SaveData Load(int slot)
    {
        LogSystem.Instance.Log("Loading...", LogType.Debug, _logTag);
        try
        {
            string path = GetPath(slot);
            if (File.Exists(path))
            {
                string json = File.ReadAllText(path);
                LogSystem.Instance.Log("Loaded Sucessfully.", LogType.Debug, _logTag);
                GameDataSystem.Instance.CurrentSaveData = JsonUtility.FromJson<SaveData>(json);
            }
            else
            {
                LogSystem.Instance.Log($"No Save Found.\nCreating New Save...", LogType.Debug, _logTag);
                GameDataSystem.Instance.CurrentSaveData = new SaveData();
                LogSystem.Instance.Log($"New Save Creation Successfull...", LogType.Debug, _logTag);
            }
        }
        catch (Exception e)
        {
            LogSystem.Instance.Log($"Error: {e.Message}", LogType.Error, _logTag);
            return null;
        }
        return GameDataSystem.Instance.CurrentSaveData;
    }
    public bool DeleteSave(int slot)
    {
        if (!SaveExists(slot)) { return true; }
        try
        {
            File.Delete(GetPath(slot));
            return true;
        }
        catch (Exception e)
        {
            LogSystem.Instance.Log($"Error: {e.Message}", LogType.Error, _logTag);
            return false;
        }
    }
    private string GetPath(int slot)
    {
        return Path.Combine(saveFolderPath, $"save{slot}.json");
    }
    public bool SaveExists(int slot)
    {
        return File.Exists(GetPath(slot));
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
            inputActionAsset.LoadBindingOverridesFromJson(rebinds);
    }

    public void SaveControls()
    {
        LogSystem.Instance.Log("Saving control bindings to PlayerPrefs.", LogType.Debug, _logTag);
        string rebinds = inputActionAsset.SaveBindingOverridesAsJson();
        PlayerPrefs.SetString(PlayerPrefKeys.CONTROLS_KEY, rebinds);
        PlayerPrefs.Save();
    }
}

