using System;
using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class InGameManager : MonoBehaviour
{
    [Header("Utility")]
    [SerializeField] private PlayerInput playerInput;
    [SerializeField] private EventSystem eventSystem;
    [SerializeField] private GameObject globalVolume;
    [SerializeField] private GameObject globalLight2D;

    [Header("Enviroment")]
    [SerializeField] private new Camera camera;
    [SerializeField] private CinemachineCamera cinemachine;
    [SerializeField] private GameObject background;

    [Header("InGame")]
    [SerializeField] private InGameCanvasLogic inGame;

    [Header("Player")]
    [SerializeField] private PlayerController player;

    [Header("Overlays")]
    [SerializeField] private SettingsManager settings;
    [SerializeField] private DeathScreenLogic deathScreen;
    [SerializeField] private CompleteScreenLogic completeScreen;
    [SerializeField] private PauseMenuLogic pauseMenu;

    private static Spaceship spaceship;
    private static LevelData levelData;

    private static bool isPaused;
    private static bool isSettingsActive;
    private static bool isPlayerAlive;

    private void OnDestroy()
    {
        PauseMenuLogic.OnResumeGame -= ResumeGame;
    }

    private void Awake()
    {
        PauseMenuLogic.OnResumeGame += ResumeGame;

        spaceship = GameDataSystem.Instance.GetSpaceship();
        levelData = GameDataSystem.Instance.GetLevelData();

        /* Instantiate */
        // Utility
        eventSystem = Instantiate(eventSystem);
        globalLight2D = Instantiate(globalLight2D);
        globalVolume = Instantiate(globalVolume);

        // Enviroment
        camera = Instantiate(camera);
        cinemachine = Instantiate(cinemachine);
        background = Instantiate(background);
        
        // InGame
        inGame = Instantiate(inGame);
        
        // Player
        player = Instantiate(player);
        player.Initialize(spaceship, playerInput);
        cinemachine.Follow = player.transform;
        
        // Overlays
        //settings = Instantiate(settings);
        //deathScreen = Instantiate(deathScreen);
        //completeScreen = Instantiate(completeScreen);
        //pauseMenu = Instantiate(pauseMenu);


        isPaused = false;
        isSettingsActive = false;
        isPlayerAlive = true;
    }

    private void Start()
    {
        string levelName = $"Chapter {GameDataSystem.currentChapter} - Level {GameDataSystem.currentLevel}";
        int numEnemiesLeft = levelData.NumberOfEnemies;
        float timer = levelData.TimeLimit;
        inGame.Initialize(levelName, numEnemiesLeft, timer);
    }

    private void OnSettingsClosedHandler()
    {
        isSettingsActive = false;
        settings.gameObject.SetActive(false);
        pauseMenu.gameObject.SetActive(true);
    }

    private void OnOpenSettingsHandler()
    {
        LogSystem.Instance.Log("Opening Settings", LogType.Info, "InGameManager");
        isSettingsActive = true;
        pauseMenu.gameObject.SetActive(false);
        settings.gameObject.SetActive(true);
    }

    public void OnPauseHandler(InputAction.CallbackContext context)
    {
        if (context.performed && isPlayerAlive && !isSettingsActive)
        {
            if (isPaused)
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
        pauseMenu.gameObject.SetActive(true);
        isPaused = !isPaused;
    }

    private void ResumeGame()
    {
        LogSystem.Instance.Log("Game Resumed", LogType.Info, "GameManager");
        Time.timeScale = 1f;
        pauseMenu.gameObject.SetActive(false);
        isPaused = !isPaused;
    }

    public static event Action<InputAction.CallbackContext> OnMove;
    public void OnMoveHandler(InputAction.CallbackContext context)
    {
        //player.Move(context.ReadValue<Vector2>());
    }

    public static event Action<InputAction.CallbackContext> OnShoot;
    public void OnShootHandler(InputAction.CallbackContext context)
    {
        if(!context.performed) { return; }
        OnShoot?.Invoke(context);
    }
}
