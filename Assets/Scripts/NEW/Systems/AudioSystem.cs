using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Audio;

public class AudioSystem : Singleton<AudioSystem>, ISystem
{
    const string _logTag = "AudioSystem";

    // Audio Mixer
    [Header("Audio System")]
    [SerializeField] AudioMixer audioMixer;

    // Audio Mixer Keys
    const string MIXER_MASTER_VOLUME_KEY = "Master Volume";
    const string MIXER_MUSIC_VOLUME_KEY = "Music Volume";
    const string MIXER_SFX_VOLUME_KEY = "SFX Volume";

    // Audio Sources
    [Header("Audio Sources")]
    [SerializeField] AudioSource musicAudioSource;
    [SerializeField] AudioSource sfxAudioSource;

    public float defaultVolume { get; private set; } = 0.5f;
    public static event Action OnSystemInitialized;

    // Initialize System
    public IEnumerator Initialize()
    {
        LogSystem.Instance.Log("Initializing AudioSystem...", LogType.Info, _logTag);

        if (AudioSystem.Instance == null) { yield return null; }

        OnSystemInitialized?.Invoke();
    }
    // Initialize Audio
    public void InitializeAudio()
    {
        LogSystem.Instance.Log("Initializing Audio...", LogType.Info, _logTag);
        SetMixerMasterVolume(SaveSystem.Instance.GetMasterVolume(defaultVolume));
        SetMixerMusicVolume(SaveSystem.Instance.GetMusicVolume(defaultVolume));
        SetMixerSfxVolume(SaveSystem.Instance.GetSfxVolume(defaultVolume));
    }
    public void Reset()
    {
        SetMixerMasterVolume(defaultVolume);
        SetMixerMusicVolume(defaultVolume);
        SetMixerSfxVolume(defaultVolume);
    }

    /*
     * Audio Methods
     */
    public void PlayMusic(AudioClip music, bool loop) 
    { 
        musicAudioSource.clip = music;
        musicAudioSource.loop = loop;
        musicAudioSource.Play(); 
    }
    public void StopMusic() { musicAudioSource.Stop(); }
    public void PlaySfx(AudioClip clip) { Play(sfxAudioSource, clip); }
    void Play(AudioSource source, AudioClip clip) { source.PlayOneShot(clip); }

    /*
     * Mixer Methods
     */
    // Setters
    public void SetMixerMasterVolume(float volume)
    {
        volume = volume <= 0 ? 0.0000001f : volume;
        audioMixer.SetFloat(MIXER_MASTER_VOLUME_KEY, Mathf.Log10(volume) * 20f);
    }
    public void SetMixerMusicVolume(float volume)
    {
        volume = volume <= 0 ? 0.0000001f : volume;
        audioMixer.SetFloat(MIXER_MUSIC_VOLUME_KEY, Mathf.Log10(volume) * 20f);
    }
    public void SetMixerSfxVolume(float volume)
    {
        volume = volume <= 0 ? 0.0000001f : volume;
        audioMixer.SetFloat(MIXER_SFX_VOLUME_KEY, Mathf.Log10(volume) * 20f);
    }
    //Getters
    public float GetMixerMasterVolume()
    {
        audioMixer.GetFloat(MIXER_MASTER_VOLUME_KEY, out float volume);
        return Mathf.Pow(10f, volume / 20f);
    }
    public float GetMixerMusicVolume()
    {
        audioMixer.GetFloat(MIXER_MUSIC_VOLUME_KEY, out float volume);        
        return Mathf.Pow(10f, volume / 20f);
    }
    public float GetMixerSfxVolume()
    {
        audioMixer.GetFloat(MIXER_SFX_VOLUME_KEY, out float volume);        
        return Mathf.Pow(10f, volume / 20f);
    }

    /*
     * Save
     */
    public void SaveAudioSettings()
    {
        SaveSystem.Instance.SaveAudioSettings(GetMixerMasterVolume(), GetMixerMusicVolume(), GetMixerSfxVolume());
    }
}
