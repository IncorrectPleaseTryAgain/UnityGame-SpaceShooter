using UnityEngine;
using UnityEngine.EventSystems;

public class LevelSelectManager : MonoBehaviour
{
    [Header("Utility")]
    [SerializeField] private EventSystem eventSystem;

    [Header("Enviroment")]
    [SerializeField] private new Camera camera;
    [SerializeField] private GameObject background;

    [Header("Level Select")]
    [SerializeField] private LevelSelectCanvasLogic levelSelect;

    [Header("Settings")]
    [SerializeField] private SettingsManager settings;

    private void OnDestroy()
    {
        SettingsManager.OnSettingsClosed -= OnSettingsClosedHandler;
        SettingsManager.OnSettingsOpened -= OnSettingsOpenedHandler;
    }

    private void Awake()
    {
        SettingsManager.OnSettingsClosed += OnSettingsClosedHandler;
        SettingsManager.OnSettingsOpened += OnSettingsOpenedHandler;

        Instantiate(eventSystem);
        camera = Instantiate(camera);
        Instantiate(background, camera.transform);
        levelSelect = Instantiate(levelSelect);
        settings = Instantiate(settings);
    }

    private void OnSettingsClosedHandler()
    {
        levelSelect.gameObject.SetActive(true);
    }
    private void OnSettingsOpenedHandler()
    {
        levelSelect.gameObject.SetActive(false);
    }
}
