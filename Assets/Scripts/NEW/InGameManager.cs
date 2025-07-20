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

    [Header("Overlays")]
    [SerializeField] Canvas _settingsCanvas;
    [SerializeField] Canvas _deathScreenCanvas;
    [SerializeField] Canvas _completeScreenCanvas;
    [SerializeField] Canvas _pauseMenuCanvas;

    bool _isPaused = false;

    public static event Action OnOpenSettings;
    public static event Action OnCloseSettings;

    private void OnDestroy()
    {
        SettingsManager.OnSettingsIsActive -= OnSettingsIsActiveHandler;
    }



    private void Awake()
    {
        SettingsManager.OnSettingsIsActive += OnSettingsIsActiveHandler;
        Initialize();
    }

    private void Initialize()
    {
        _camera = Instantiate(_camera);
        _cinemachineCamera = Instantiate(_cinemachineCamera);
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
    }

    private void OnSettingsIsActiveHandler(bool obj)
    {
        LogSystem.Instance.Log("Settings Active: " + obj, LogType.Info, "InGameManager");
        _isPaused = obj;
    }

    public void OnPauseHandler(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            if (_isPaused)
            {
                OnOpenSettings?.Invoke();
                ResumeGame();
            }
            else
            {
                OnCloseSettings?.Invoke();
                PauseGame();
            }
            _isPaused = !_isPaused;
        }
    }

    private void PauseGame()
    {
        LogSystem.Instance.Log("Game Paused", LogType.Info, "GameManager");
        Time.timeScale = 0f;
        _settingsCanvas.gameObject.SetActive(true);
    }

    private void ResumeGame()
    {
        LogSystem.Instance.Log("Game Resumed", LogType.Info, "GameManager");
        Time.timeScale = 1f;
        _settingsCanvas.gameObject.SetActive(false);
    }
}
