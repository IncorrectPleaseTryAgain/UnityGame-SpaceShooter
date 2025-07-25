using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class MainMenuManager : MonoBehaviour
{
    static readonly string _logTag = "MainMenuManager";

    [Header("Utility")]
    [SerializeField] private PlayerInput playerInput;
    [SerializeField] private EventSystem eventSystem;
    [SerializeField] private Systems systems;

    [Header("Enviroment")]
    [SerializeField] private new Camera camera;
    [SerializeField] private GameObject background;

    [Header("Main Menu")]
    [SerializeField] private AudioClip ACLP_MainMenu;
    [SerializeField] private GameObject mainMenuCanvas;

    [Header("Settings")]
    [SerializeField] private SettingsManager _settings;

    private bool canContinue = false;

    public static event Action OnContinue;
    private void OnDestroy()
    {
        MainMenuCanvasHeaderLogic.OnFadeInAnimationComplete -= MainMenuCanvasHeaderOnAnimationCompleteHandler;
        SettingsManager.OnSettingsClosed -= OnSettingsClosedHandler;
        SettingsManager.OnSettingsOpened -= OnSettingsOpenedHandler;

    }
    private void Awake()
    {
        MainMenuCanvasHeaderLogic.OnFadeInAnimationComplete += MainMenuCanvasHeaderOnAnimationCompleteHandler;
        Systems.OnInitialized += AllSystemsInitializedHandler; // removed after initialization
        SettingsManager.OnSettingsClosed += OnSettingsClosedHandler;
        SettingsManager.OnSettingsOpened += OnSettingsOpenedHandler;

        playerInput.enabled = false;

        if (Systems.Instance != null) { AllSystemsInitializedHandler(); return; }
        Instantiate(systems).Initialize();
    }
    private void Start()
    {
        AudioSystem.Instance.InitializeAudio();
        VideoSystem.Instance.InitializeVideo();
    }
    public void Continue()
    {
        if (!canContinue) { return; }
        canContinue = false;

        AudioSystem.Instance.PlayMusic(ACLP_MainMenu, true);
        eventSystem.enabled = true;
        _settings.gameObject.SetActive(true);

        OnContinue?.Invoke();
    }

    private void OnSettingsOpenedHandler()
    {
        mainMenuCanvas.gameObject.SetActive(false);
    }

    private void OnSettingsClosedHandler()
    {
        mainMenuCanvas.gameObject.SetActive(true);
    }

    private void AllSystemsInitializedHandler()
    {
        LogSystem.Instance.Log("All Systems Initialized", LogType.Info, _logTag);
        Systems.OnInitialized -= AllSystemsInitializedHandler;

        camera = Instantiate(camera);
        
        Instantiate(background, camera.transform);

        mainMenuCanvas = Instantiate(mainMenuCanvas);

        _settings = Instantiate(_settings);
        _settings.gameObject.SetActive(false);

        eventSystem = Instantiate(eventSystem);
        eventSystem.enabled = false;
    }

    private void MainMenuCanvasHeaderOnAnimationCompleteHandler()
    {
        playerInput.enabled = true;
        canContinue = true;
    }
}
