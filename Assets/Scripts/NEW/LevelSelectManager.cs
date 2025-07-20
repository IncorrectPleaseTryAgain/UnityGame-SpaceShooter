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
        SettingsManager.OnSettingsIsActive -= OnSettingsIsActiveHandler;
    }

    private void Awake()
    {
        SettingsManager.OnSettingsIsActive += OnSettingsIsActiveHandler;
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
    void OnSettingsIsActiveHandler(bool isActive)
    {
        _levelSelectCanvas.gameObject.SetActive(!isActive);
    }
}
