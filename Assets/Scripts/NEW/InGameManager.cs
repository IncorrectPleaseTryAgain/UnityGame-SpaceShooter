using System;
using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;

public class InGameManager : MonoBehaviour
{
    [Header("Utility")]
    [SerializeField] PlayerInput _playerInput;

    [Header("Enviroment")]
    [SerializeField] Camera _camera;
    [SerializeField] CinemachineCamera _cinemachineCamera;
    [SerializeField] GameObject _globalVolume;
    [SerializeField] GameObject _globalLight2D;
    [SerializeField] GameObject _background;

    [Header("InGame")]
    [SerializeField] Canvas _inGameCanvas;

    [Header("Player")]
    [SerializeField] PlayerController _player;

    [Header("Overlays")]
    [SerializeField] Canvas _settingsCanvas;
    [SerializeField] Canvas _deathScreenCanvas;
    [SerializeField] Canvas _completeScreenCanvas;
    [SerializeField] Canvas _pauseMenuCanvas;

    bool _isPaused;
    bool _isSettingsActive;
    bool _isPlayerAlive;
    LevelData _currentLevelData;


    public static event Action OnOpenSettings;
    public static event Action OnCloseSettings;

    private void OnDestroy()
    {
        SettingsManager.OnSettingsClosed -= OnSettingsClosedHandler;
        PauseMenuCanvasLogic.OnOpenSettings -= OnOpenSettingsHandler;
        PauseMenuCanvasLogic.OnResumeGame -= ResumeGame;
    }

    private void Awake()
    {
        SettingsManager.OnSettingsClosed += OnSettingsClosedHandler;
        PauseMenuCanvasLogic.OnOpenSettings += OnOpenSettingsHandler;
        PauseMenuCanvasLogic.OnResumeGame += ResumeGame;
        Initialize();
    }

    private void Start()
    {
        //string levelName = $"Chapter {GameDataSystem.Instance.currentChapter} - Level {GameDataSystem.Instance.currentLevel}";
        int numEnemiesLeft = _currentLevelData.NumberOfEnemies;
        float timer = _currentLevelData.TimeLimit;
        //_inGameCanvas.GetComponent<InGameCanvasLogic>().Initialize(levelName, numEnemiesLeft, timer);
    }

    private void Initialize()
    {
        //_currentLevelData = GameDataSystem.Instance.GetLevelData();

        _camera = Instantiate(_camera);
        _cinemachineCamera = Instantiate(_cinemachineCamera);

        _player = Instantiate(_player);
        _player.Initialize(_currentLevelData.spaceship, _playerInput);

        _cinemachineCamera.GetComponent<CinemachineCamera>().Follow = _player.transform;

        _globalVolume = Instantiate(_globalVolume);
        _globalLight2D = Instantiate(_globalLight2D);
        _background = Instantiate(_background);
        _inGameCanvas = Instantiate(_inGameCanvas);

        _settingsCanvas = Instantiate(_settingsCanvas);
        _settingsCanvas.gameObject.SetActive(false);

        _deathScreenCanvas = Instantiate(_deathScreenCanvas);
        _deathScreenCanvas.gameObject.SetActive(false);

        _completeScreenCanvas = Instantiate(_completeScreenCanvas);
        _completeScreenCanvas.gameObject.SetActive(false);

        _pauseMenuCanvas = Instantiate(_pauseMenuCanvas);
        _pauseMenuCanvas.gameObject.SetActive(false);

        _isPaused = false;
        _isSettingsActive = false;
        _isPlayerAlive = true;
    }

    private void OnSettingsClosedHandler()
    {
        _isSettingsActive = false;
        _settingsCanvas.gameObject.SetActive(false);
        _pauseMenuCanvas.gameObject.SetActive(true);
    }

    private void OnOpenSettingsHandler()
    {
        LogSystem.Instance.Log("Opening Settings", LogType.Info, "InGameManager");
        _isSettingsActive = true;
        _pauseMenuCanvas.gameObject.SetActive(false);
        _settingsCanvas.gameObject.SetActive(true);
    }

    public void OnPauseHandler(InputAction.CallbackContext context)
    {
        if (context.performed && _isPlayerAlive && !_isSettingsActive)
        {
            if (_isPaused)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }
        }
    }

    private void PauseGame()
    {
        LogSystem.Instance.Log("Game Paused", LogType.Info, "GameManager");
        Time.timeScale = 0f;
        _pauseMenuCanvas.gameObject.SetActive(true);
        _isPaused = !_isPaused;
    }

    private void ResumeGame()
    {
        LogSystem.Instance.Log("Game Resumed", LogType.Info, "GameManager");
        Time.timeScale = 1f;
        _pauseMenuCanvas.gameObject.SetActive(false);
        _isPaused = !_isPaused;
    }

    public static event Action<InputAction.CallbackContext> OnMove;
    public void OnMoveHandler(InputAction.CallbackContext context)
    {
        OnMove?.Invoke(context);
    }

    public static event Action<InputAction.CallbackContext> OnShoot;
    public void OnShootHandler(InputAction.CallbackContext context)
    {
        if(!context.performed) { return; }
        OnShoot?.Invoke(context);
    }
}
