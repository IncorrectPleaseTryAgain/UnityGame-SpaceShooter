using System;
using System.Collections;
using UnityEngine;

public class Systems : PersistentSingleton<Systems>
{
    const string _logTag = "Systems";
    [Header("Systems")]
    [SerializeField] LogSystem _logSystem;
    [SerializeField] SaveSystem _saveSystem;
    [SerializeField] SceneSystem _sceneSystem;
    [SerializeField] StateSystem _stateSystem;
    [SerializeField] AudioSystem _audioSystem;
    [SerializeField] VideoSystem _videoSystem;

    int systemsInitialized = 0;
    const int NUMBER_OF_SYSTEMS = 6; // 6 systems

    public static event Action OnSystemsFinishedInitialization;

    private void OnEnable()
    {
        AddEventListeners();
    }
    private void OnDisable()
    {
        RemoveEventListeners();
    }
    public void Initialize()
    {
        Debug.Log("Initializing Systems...");

        StartCoroutine(_logSystem.Initialize());
        StartCoroutine(_saveSystem.Initialize());
        StartCoroutine(_sceneSystem.Initialize());
        StartCoroutine(_stateSystem.Initialize());
        StartCoroutine(_audioSystem.Initialize());
        StartCoroutine(_videoSystem.Initialize());
    }
    void CallAllCrossDependentInitializationMethods()
    {
        LogSystem.Instance.Log("Calling All Cross-Dependent Initialization Methods", LogType.Info, _logTag);

        _audioSystem.InitializeAudio(); // Cross Dependent with Save System
        _videoSystem.InitializeVideo(); // Cross Dependent with Save System

        RemoveEventListeners();
        OnSystemsFinishedInitialization?.Invoke();
    }

    public void RegisterInitialized()
    {
        systemsInitialized++;
        LogSystem.Instance.Log("Initialzed System | Progress: " + ((systemsInitialized * 100) / NUMBER_OF_SYSTEMS) + "%", LogType.Info, _logTag);
        if (systemsInitialized == NUMBER_OF_SYSTEMS) { CallAllCrossDependentInitializationMethods(); }
    }


    void AddEventListeners()
    {
        LogSystem.OnSystemInitialized += RegisterInitialized;
        SaveSystem.OnSystemInitialized += RegisterInitialized;
        SceneSystem.OnSystemInitialized += RegisterInitialized;
        StateSystem.OnSystemInitialized += RegisterInitialized;
        AudioSystem.OnSystemInitialized += RegisterInitialized;
        VideoSystem.OnSystemInitialized += RegisterInitialized;
    }
    void RemoveEventListeners()
    {
        LogSystem.OnSystemInitialized -= RegisterInitialized;
        SaveSystem.OnSystemInitialized -= RegisterInitialized;
        SceneSystem.OnSystemInitialized -= RegisterInitialized;
        StateSystem.OnSystemInitialized -= RegisterInitialized;
        AudioSystem.OnSystemInitialized -= RegisterInitialized;
        VideoSystem.OnSystemInitialized -= RegisterInitialized;
    }
}
