using System;
using UnityEngine;

public class ChapterSelectManager : MonoBehaviour
{
    [Header("Enviroment")]
    [SerializeField] Camera _camera;
    [SerializeField] GameObject _background;

    [Header("Level Select")]
    [SerializeField] Canvas _chapterSelectCanvas;

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
        _chapterSelectCanvas.GetComponent<ChapterSelectCanvasLogic>().Initialize();
        _chapterSelectCanvas = Instantiate(_chapterSelectCanvas);
        _settingsCanvas = Instantiate(_settingsCanvas);
    }

    void OnSettingsIsActiveHandler(bool isActive)
    {
        _chapterSelectCanvas.gameObject.SetActive(!isActive);
    }

}
