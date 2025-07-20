using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class MainMenuManager : MonoBehaviour
{
    const string _logTag = "MainMenuManager";

    [Header("Utility")]
    [SerializeField] PlayerInput _playerInput;
    [SerializeField] EventSystem _eventSystem;
    [SerializeField] Systems _systems;

    [Header("Enviroment")]
    [SerializeField] Camera _camera;
    [SerializeField] GameObject _background;

    [Header("Main Menu")]
    [SerializeField] AudioClip _mainMenuMusic;
    [SerializeField] GameObject _mainMenuCanvas;
    MainMenuCanvasLogic _mainMenuCanvasLogic;

    [Header("Overlays")]
    [SerializeField] GameObject _settingsCanvas;

    InputAction continueAction;

    private void OnDestroy()
    {
        MainMenuCanvasHeaderLogic.OnFadeInAnimationComplete -= MainMenuCanvasHeaderOnAnimationCompleteHandler;
        SettingsManager.OnSettingsClosed -= OnSettingsClosedHandler;

    }

    private void Awake()
    {
        MainMenuCanvasHeaderLogic.OnFadeInAnimationComplete += MainMenuCanvasHeaderOnAnimationCompleteHandler;
        Systems.OnSystemsFinishedInitialization += AllSystemsInitializedHandler; // removed after initialization
        SettingsManager.OnSettingsClosed += OnSettingsClosedHandler;
        _playerInput.enabled = false;
    }

    private void Start()
    {
        if(Systems.Instance == null)
        {
            InitializeSystems();
        }
        else
        {
            AllSystemsInitializedHandler();
        }
    }

    void InitializeSystems()
    {
        if (!_systems)
        {
            LogSystem.Instance.Log("Systems object is not assigned in the MainMenuManager.", LogType.Error, _logTag);
            throw new NullReferenceException("_systems");
        }
        Instantiate(_systems).Initialize();
    }

    private void Update()
    {
        if (continueAction != null)
        {
            if (continueAction.WasPressedThisFrame())
            {
                Continue();
            }
        }
    }
    void Continue()
    {
        LogSystem.Instance.Log("Continue...", LogType.Info, _logTag);

        continueAction = null;
        
        _playerInput.SwitchCurrentActionMap(ActionMap.GetActionMap(ActionMap.ActionMaps.InGame));
        AudioSystem.Instance.PlayMusic(_mainMenuMusic, true);
        _mainMenuCanvasLogic.Continue();
        InitializeEventSystem();
    }

    public void OpenSettingsHandler()
    {
        _settingsCanvas.gameObject.SetActive(true);
        _mainMenuCanvasLogic.SetActive(false);
    }

    void OnSettingsClosedHandler()
    {
        _mainMenuCanvasLogic.SetActive(true);
    }

    void AllSystemsInitializedHandler()
    {
        LogSystem.Instance.Log("All Systems Initialized", LogType.Info, _logTag);
        Systems.OnSystemsFinishedInitialization -= AllSystemsInitializedHandler;

        SaveSystem.Instance.currentGameSave = 0; // Default to save 0
        SaveSystem.Instance.currentChapter = 1; // Default to chapter 1
        SaveSystem.Instance.currentLevel = 1; // Default to level 1

        InitializeCamera();

        InstantiateObject(_background, _camera.transform);
        _mainMenuCanvas = InstantiateObject(_mainMenuCanvas);
        //_settingsCanvas = InstantiateObject(_settingsCanvas, _mainMenuCanvas.transform);
        _settingsCanvas = InstantiateObject(_settingsCanvas);
        _mainMenuCanvasLogic = _mainMenuCanvas.GetComponent<MainMenuCanvasLogic>();
    }

    private void InitializeEventSystem()
    {
        LogSystem.Instance.Log("Instantiating EventSystem", LogType.Info, _logTag);
        if (_eventSystem == null) { LogSystem.Instance.Log("_eventSystem Null Reference", LogType.Error, _logTag); throw new NullReferenceException("_eventSystem"); }
        _eventSystem = Instantiate(_eventSystem);
    }

    private void InitializeCamera()
    {
        LogSystem.Instance.Log("Instantiating Camera", LogType.Info, _logTag);
        if (_camera == null) { LogSystem.Instance.Log("_camera Null Reference", LogType.Error, _logTag); throw new NullReferenceException("_camera"); }

        _camera = Instantiate(_camera);
    }

    GameObject InstantiateObject(GameObject obj, Transform parent = null)
    {
        LogSystem.Instance.Log("Instantiating " + obj.name, LogType.Info, _logTag);
        if(obj == null) { LogSystem.Instance.Log("InstantiateObject(obj) Null Reference", LogType.Error, _logTag); throw new NullReferenceException(obj.name); }

        if (parent) { obj = Instantiate(obj, parent); }
        else { obj = Instantiate(obj); }

        return obj;
    }

    void MainMenuCanvasHeaderOnAnimationCompleteHandler()
    {
        LogSystem.Instance.Log("Enabling Continue Action", LogType.Info, _logTag);
        _playerInput.enabled = true;
        _playerInput.SwitchCurrentActionMap(ActionMap.GetActionMap(ActionMap.ActionMaps.MainMenu));

        continueAction = _playerInput.actions["Continue"];
    }
}
