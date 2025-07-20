using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class LevelSelectManager : MonoBehaviour
{
    [Header("Enviroment")]
    [SerializeField] Camera _camera;
    [SerializeField] GameObject _background;

    [Header("Level Select")]
    [SerializeField] Canvas _levelSelectCanvas;

    [Header("Overlays")]
    [SerializeField] GameObject _settingsCanvas;

    private void OnDestroy()
    {
        SettingsManager.OnSettingsClosed -= OnSettingsClosedHandler;
    }

    private void Awake()
    {
        SettingsManager.OnSettingsClosed += OnSettingsClosedHandler;
    }


    private void Start()
    {
        Initialize();
    }

    private void Initialize()
    {
        Instantiate(_camera);
        Instantiate(_background);
        _levelSelectCanvas.GetComponent<LevelSelectCanvasLogic>().Initialize();
        _levelSelectCanvas = Instantiate(_levelSelectCanvas);
        _settingsCanvas = Instantiate(_settingsCanvas);
    }
    void OnSettingsClosedHandler()
    {
        _levelSelectCanvas.gameObject.SetActive(true);
    }
}
