using System;
using System.Collections;
using UnityEngine;

public class Systems : PersistentSingleton<Systems>
{
    [Header("Systems")]
    [SerializeField] LogSystem _logSystem;
    [SerializeField] SaveSystem _saveSystem;
    [SerializeField] SceneSystem _sceneSystem;
    [SerializeField] StateSystem _stateSystem;
    [SerializeField] AudioSystem _audioSystem;
    [SerializeField] VideoSystem _videoSystem;

    int systemsInitialized = 0;
    const int NUMBER_OF_SYSTEMS = 6;

    public static event Action OnAllSystemsInitialized;
    //bool initializationComplete = false;

    private void OnEnable()
    {
        LogSystem.OnSystemInitialized += RegisterInitialized;
        SaveSystem.OnSystemInitialized += RegisterInitialized;
        SceneSystem.OnSystemInitialized += RegisterInitialized;
        StateSystem.OnSystemInitialized += RegisterInitialized;
        AudioSystem.OnSystemInitialized += RegisterInitialized;
        VideoSystem.OnSystemInitialized += RegisterInitialized;
    }

    private void OnDisable()
    {
        LogSystem.OnSystemInitialized -= RegisterInitialized;
        SaveSystem.OnSystemInitialized -= RegisterInitialized;
        SceneSystem.OnSystemInitialized -= RegisterInitialized;
        StateSystem.OnSystemInitialized -= RegisterInitialized;
        AudioSystem.OnSystemInitialized -= RegisterInitialized;
        VideoSystem.OnSystemInitialized -= RegisterInitialized;
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

        CallAllCrossDependentInitializationMethods();
    }
    void CallAllCrossDependentInitializationMethods()
    {
        _audioSystem.InitializeVolume(); // Cross Dependent with Save System
    }

    public void RegisterInitialized()
    {
        systemsInitialized++;
        Debug.Log("Initialzed System | Progress: " + ((systemsInitialized * 100) / NUMBER_OF_SYSTEMS) + "%");
        if (systemsInitialized == NUMBER_OF_SYSTEMS) { OnAllSystemsInitialized.Invoke(); }
    }
}
