using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class MainMenuManager : MonoBehaviour
{
    const string _logTag = "MainMenuManager";

    [Header("Systems")]
    [SerializeField] Systems _systems;
    [SerializeField] PlayerInput _playerInput;
    
    [Header("Enviroment")]
    [SerializeField] Camera _camera;
    [SerializeField] GameObject _background;

    [Header("Main Menu")]
    [SerializeField] GameObject _mainMenuCanvas;
    [SerializeField] AudioClip _mainMenuMusic;

    [Header("Overlays")]
    [SerializeField] GameObject _settingsCanvas;

    InputAction continueAction;

    private void OnDestroy()
    {
        ButtonLogic.OnButtonAction -= HandleButtonAction;
        MainMenuCanvasHeaderLogic.OnFadeInAnimationComplete -= MainMenuCanvasHeaderOnAnimationCompleteHandler;
    }

    private void Awake()
    {
        ButtonLogic.OnButtonAction += HandleButtonAction;
        MainMenuCanvasHeaderLogic.OnFadeInAnimationComplete += MainMenuCanvasHeaderOnAnimationCompleteHandler;
        Systems.OnSystemsFinishedInitialization += AllSystemsInitializedHandler;
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
        AudioSystem.Instance.PlayMusic(_mainMenuMusic, true);
        _mainMenuCanvas.GetComponent<MainMenuCanvasLogic>().Continue();
    }

    void HandleButtonAction(Actions action)
    {
        switch (action)
        {
            case Actions.OpenSettings:
                OpenSettingsHandler();
                break;
            case Actions.CloseSettings:
                CloseSettingsHandler();
                break;
            case Actions.SaveSettings:
                SaveSettingsHandler();
                break;
        }
    }
    void OpenSettingsHandler()
    {
        _mainMenuCanvas.GetComponent<MainMenuCanvasLogic>().SetActive(false);
        _settingsCanvas.gameObject.SetActive(true);
    }
    void CloseSettingsHandler()
    {
        _settingsCanvas.gameObject.SetActive(false);
        _mainMenuCanvas.GetComponent<MainMenuCanvasLogic>().SetActive(true);
    }
    void SaveSettingsHandler()
    {
        _settingsCanvas.gameObject.SetActive(false);
        _mainMenuCanvas.GetComponent<MainMenuCanvasLogic>().SetActive(true);
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
        _playerInput = GetComponent<PlayerInput>();
        _playerInput.DeactivateInput();
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
        _playerInput.SwitchCurrentActionMap("Initializer");
        continueAction = _playerInput.actions["Continue"];
    }
}
