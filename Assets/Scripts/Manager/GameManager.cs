using System;
using TMPro;
using UnityEngine;
using System.Diagnostics;
using System.Collections.Generic;
using UnityEngine.InputSystem;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private List<GameObject> interactableEnemyPrefabs;
    [SerializeField] private List<GameObject> staticEnemyPrefabs;
    [SerializeField] private List<GameObject> enemySpawners;
    [SerializeField] private TMP_Text roundText;
    [SerializeField] private TMP_Text clockText;
    [SerializeField] private List<AudioClip> backgroundMusic;
    [SerializeField] private AudioClip roundBegin;
    [SerializeField] private AudioSource musicSource;

    [SerializeField] private int staticEnemySpawnRate = 30;
    [SerializeField] private int staticEnemySpawnPosOffset = 5;

    [SerializeField] private int currentRound;
    [SerializeField] private int numEnemies;

    private TimeSpan timeSpan;
    private Stopwatch roundStopWatch;
    private string time;

    private float musicTimer;
    private float currentMusicLength;
    private const int MIN_TIME_BETWEEN_MUSIC = 15;
    private const int MAX_TIME_BETWEEN_MUSIC = 30;

    private bool playerIsAlive;

    //InputActionAsset inputActions;
    //private InputAction pauseAction;

    [SerializeField] private bool _isPaused = false;
    public bool IsPaused
    {
        get { return _isPaused; }
        set { _isPaused = value; }
    }

    private void Awake() { StateManager.OnGameStateChanged += OnGameStateChangedHandler; }
    private void OnDestroy() { StateManager.OnGameStateChanged -= OnGameStateChangedHandler; }
    private void Start() { GameStartHandler(); }
    private void GameStartHandler()
    {
        currentRound = 0;
        numEnemies = 0;
        musicTimer = 0;

        playerIsAlive = true;

        roundStopWatch = new Stopwatch();
        roundStopWatch.Start();

        Invoke(nameof(SpawnStaticEnemy), staticEnemySpawnRate);
        StateManager.instance.UpdateGameState(GameStates.RoundStart);
    }
    void Update()
    {
        PlayMusic();
        if (playerIsAlive && !IsPaused)
        {
            UpdateRoundTime();
        }
    }

    private void UpdateRoundTime()
    {
        timeSpan = roundStopWatch.Elapsed;
        time = (timeSpan.Hours > 0) ? String.Format("{0:00}:{1:00}:{2:00}.{3:00}", timeSpan.Hours, timeSpan.Minutes, timeSpan.Seconds, timeSpan.Milliseconds / 10)
                                        : String.Format("{0:00}:{1:00}.{2:00}", timeSpan.Minutes, timeSpan.Seconds, timeSpan.Milliseconds / 10);
        clockText.text = time;
    }

    private void PlayMusic()
    {
        if (musicTimer <= 0f)
        {
            PlayRandomMusic();
            musicTimer = UnityEngine.Random.Range(currentMusicLength + MIN_TIME_BETWEEN_MUSIC, currentMusicLength + MAX_TIME_BETWEEN_MUSIC);
        }
        musicTimer -= Time.deltaTime;
    }
    private void PlayRandomMusic()
    {
        int randomMusic = UnityEngine.Random.Range(0, backgroundMusic.Count);
        musicSource.PlayOneShot(backgroundMusic[randomMusic]);

        currentMusicLength = backgroundMusic[randomMusic].length;
    }

    private void OnGameStateChangedHandler(GameStates state)
    {
        switch (state)
        {
            case GameStates.RoundStart:
                RoundStartHandler();
                break;
            case GameStates.PlayerDeath:
                PlayerDeathHandler();
                break;
            case GameStates.EnemyDeath:
                EnemyDeathHandler();
                break;
            case GameStates.GamePause:
                GamePauseHandler();
                break;
            case GameStates.GameResume:
                GameResumeHandler();
                break;
        }
    }

    private void GamePauseHandler() { roundStopWatch.Stop(); }
    private void GameResumeHandler() { roundStopWatch.Start(); }
    private void RoundStartHandler()
    {
        //AudioManager.instance.PlaySFX(roundBegin);

        currentRound++;
        roundText.text = currentRound.ToString();
        numEnemies = currentRound;
        //UnityEngine.Debug.Log("Round Start: " + numEnemies);

        for (int i = 0; i < numEnemies; i++)
        {
            int randomEnemyType = UnityEngine.Random.Range(0, interactableEnemyPrefabs.Count);
            int randomEnemySpawner = UnityEngine.Random.Range(0, enemySpawners.Count);

            enemySpawners[randomEnemySpawner].GetComponent<EnemySpawnerLogic>().SpawnEnemy(interactableEnemyPrefabs[randomEnemyType]);
        }
    }

    private void SpawnStaticEnemy()
    {
        //UnityEngine.Debug.Log("Static Enemy Spawned");
        int randomEnemyType = UnityEngine.Random.Range(0, interactableEnemyPrefabs.Count);

        GameObject enemy = staticEnemyPrefabs[randomEnemyType];

        float spawnPos_X = UnityEngine.Random.Range(player.transform.position.x - staticEnemySpawnPosOffset, player.transform.position.x + staticEnemySpawnPosOffset);
        float spawnPos_Y = UnityEngine.Random.Range(player.transform.position.y - staticEnemySpawnPosOffset, player.transform.position.y - staticEnemySpawnPosOffset);

        enemy.transform.position = new Vector2(spawnPos_X, spawnPos_Y); ;
        Instantiate(enemy);

        if (playerIsAlive) { Invoke(nameof(SpawnStaticEnemy), staticEnemySpawnRate); }
    }

    private void EnemyDeathHandler()
    {
        if (playerIsAlive)
        {
            numEnemies--;
            if (numEnemies <= 0) { StateManager.instance.UpdateGameState(GameStates.RoundStart); }
        }
    }

    private void PlayerDeathHandler()
    {
        SaveManager.instance.SaveRound(currentRound, time);

        playerIsAlive = false;
    }
}
