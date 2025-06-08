using System;
using TMPro;
using UnityEngine;
using System.Diagnostics;
using System.Collections.Generic;
using UnityEngine.InputSystem;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private List<GameObject> enemyPrefabs;
    [SerializeField] private List<GameObject> enemySpawners;
    [SerializeField] private TMP_Text roundText;
    [SerializeField] private TMP_Text clockText;
    [SerializeField] private List<AudioClip> backgroundMusic;
    [SerializeField] private AudioClip roundBegin;
    [SerializeField] private AudioSource musicSource;

    private int currentRound;
    private int numEnemies;

    //public TimeSpan timeElapsed { get; private set; }
    private TimeSpan timeSpan;
    private Stopwatch stopwatch;
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

        stopwatch = new Stopwatch();
        stopwatch.Start();

        StateManager.instance.UpdateGameState(GameStates.RoundStart);
    }
    void Update()
    {
        PlayMusic();
        if (playerIsAlive)
        {
            if (!IsPaused)
            {
                // Timer
                timeSpan = stopwatch.Elapsed;
                time = (timeSpan.Hours > 0) ? String.Format("{0:00}:{1:00}:{2:00}.{3:00}", timeSpan.Hours, timeSpan.Minutes, timeSpan.Seconds, timeSpan.Milliseconds / 10)
                                             : String.Format("{0:00}:{1:00}.{2:00}", timeSpan.Minutes, timeSpan.Seconds, timeSpan.Milliseconds / 10);
                clockText.text = time;
            }
        }
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
        }
    }

    private void RoundStartHandler()
    {
        AudioManager.instance.PlaySoundQueue(roundBegin);

        currentRound++;
        roundText.text = currentRound.ToString();

        numEnemies = currentRound;
        for (int i = 0; i < numEnemies; i++)
        {
            int randomEnemyType = UnityEngine.Random.Range(0, enemyPrefabs.Count);
            int randomEnemySpawner = UnityEngine.Random.Range(0, enemySpawners.Count);

            enemySpawners[randomEnemySpawner].GetComponent<EnemySpawnerLogic>().SpawnEnemy(enemyPrefabs[randomEnemyType]);
        }
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
