using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class MainMenuManager : MonoBehaviour
{
    const string _logTag = "MainMenuManager";

    [Header("Systems")]
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
        SettingsManager.OnSettingsIsActive -= OnSettingsIsActiveHandler;

    }

    private void Awake()
    {
        MainMenuCanvasHeaderLogic.OnFadeInAnimationComplete += MainMenuCanvasHeaderOnAnimationCompleteHandler;
        Systems.OnSystemsFinishedInitialization += AllSystemsInitializedHandler; // removed after initialization
        SettingsManager.OnSettingsIsActive += OnSettingsIsActiveHandler;

    }

    private void Start()
    {
        InitializeSystems();
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
        InputSystem.Instance.SetActionMapActive(InputSystem.ActionMaps.InGame);
        AudioSystem.Instance.PlayMusic(_mainMenuMusic, true);
        _mainMenuCanvasLogic.Continue();
    }

    public void OpenSettingsHandler()
    {
        _settingsCanvas.gameObject.SetActive(true);
        _mainMenuCanvasLogic.SetActive(false);
    }

    void OnSettingsIsActiveHandler(bool isActive)
    {
        LogSystem.Instance.Log("Settings Active: " + isActive, LogType.Info, _logTag);
        if (isActive)
        {
            _settingsCanvas.gameObject.SetActive(true);
            _mainMenuCanvasLogic.SetActive(false);
        }
        else
        {
            _settingsCanvas.gameObject.SetActive(false);
            _mainMenuCanvasLogic.SetActive(true);
        }
    }

    void AllSystemsInitializedHandler()
    {
        LogSystem.Instance.Log("All Systems Initialized", LogType.Info, _logTag);
        Systems.OnSystemsFinishedInitialization -= AllSystemsInitializedHandler;

        InitializePlayerInput();
        InitializeCamera();

        InstantiateObject(_background, _camera.transform);
        _mainMenuCanvas = InstantiateObject(_mainMenuCanvas);
        _settingsCanvas = InstantiateObject(_settingsCanvas, _mainMenuCanvas.transform);
        _mainMenuCanvasLogic = _mainMenuCanvas.GetComponent<MainMenuCanvasLogic>();
    }

    private void InitializeCamera()
    {
        LogSystem.Instance.Log("Instantiating Camera", LogType.Info, _logTag);
        if (_camera == null) { LogSystem.Instance.Log("_camera Null Reference", LogType.Error, _logTag); throw new NullReferenceException("_camera"); }

        _camera = Instantiate(_camera);
    }

    void InitializePlayerInput()
    {
        LogSystem.Instance.Log("Initializing Player Input", LogType.Info, _logTag);
        continueAction = null;
        InputSystem.Instance.SetInputActive(false);
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
        InputSystem.Instance.SetInputActive(true);
        InputSystem.Instance.SetActionMapActive(InputSystem.ActionMaps.Continue);
        continueAction = InputSystem.Instance.GetAction("Continue");
    }
}
