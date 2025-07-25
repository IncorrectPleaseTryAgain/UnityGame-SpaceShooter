using System;
using System.Collections;
using UnityEngine;

public class Systems : PersistentSingleton<Systems>
{
    static readonly string _logTag = "Systems";

    [Header("Systems")]
    [SerializeField] private LogSystem _logSystem;
    [SerializeField] private SaveSystem _saveSystem;
    [SerializeField] private SceneSystem _sceneSystem;
    [SerializeField] private StateSystem _stateSystem;
    [SerializeField] private AudioSystem _audioSystem;
    [SerializeField] private VideoSystem _videoSystem;
    [SerializeField] private GameDataSystem _dataSystem;

    private int systemsInitialized = 0;
    private readonly int NUMBER_OF_SYSTEMS = 7;

    public static event Action OnInitialized;
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
        StartCoroutine(_dataSystem.Initialize());
        StartCoroutine(_saveSystem.Initialize());
        StartCoroutine(_sceneSystem.Initialize());
        StartCoroutine(_stateSystem.Initialize());
        StartCoroutine(_audioSystem.Initialize());
        StartCoroutine(_videoSystem.Initialize());
    }

    public void RegisterInitialized()
    {
        systemsInitialized++;
        LogSystem.Instance.Log(((systemsInitialized * 100) / NUMBER_OF_SYSTEMS) + "% Complete", LogType.Info, _logTag);
        if (systemsInitialized == NUMBER_OF_SYSTEMS) { OnInitialized?.Invoke(); }
    }


    void AddEventListeners()
    {
        LogSystem.OnSystemInitialized += RegisterInitialized;
        SaveSystem.OnSystemInitialized += RegisterInitialized;
        SceneSystem.OnSystemInitialized += RegisterInitialized;
        StateSystem.OnSystemInitialized += RegisterInitialized;
        AudioSystem.OnSystemInitialized += RegisterInitialized;
        VideoSystem.OnSystemInitialized += RegisterInitialized;
        GameDataSystem.OnSystemInitialized += RegisterInitialized;
    }
    void RemoveEventListeners()
    {
        LogSystem.OnSystemInitialized -= RegisterInitialized;
        SaveSystem.OnSystemInitialized -= RegisterInitialized;
        SceneSystem.OnSystemInitialized -= RegisterInitialized;
        StateSystem.OnSystemInitialized -= RegisterInitialized;
        AudioSystem.OnSystemInitialized -= RegisterInitialized;
        VideoSystem.OnSystemInitialized -= RegisterInitialized;
        GameDataSystem.OnSystemInitialized -= RegisterInitialized;
    }
}
