//using System;
//using UnityEngine;
//using System.Collections.Generic;
//using static UnityEngine.Rendering.DebugUI;

//[RequireComponent(typeof(Rigidbody2D))]
//[RequireComponent (typeof(Animator))]
//[RequireComponent (typeof(HealthLogic))]
//public class AsteroidLogic : MonoBehaviour, IHealthResponder
//{
//    private const string TAG_PLAYER = "Player";
//    private const string TAG_PROJECTILE = "Projectile";

//    [Serializable]
//    private struct Properties
//    {
//        public string name; 
//        public string description;

//        public float maxHealth;
//        public float damage;
//        public float maxSpeed;

//        public float attackDelayTimer;

//        public PositionIndicatorLogic positionIndicator;

//        public List<AudioClip> deathSFX;
//        public List<AudioClip> playerHitSFX;
//    }
//    [SerializeField] private Properties properties;

//    [SerializeField] private Rigidbody2D rb;
//    [SerializeField] private Animator anim;
//    [SerializeField] private HealthLogic health;
//    [SerializeField] private PlayerController player;
//    private float playerGravityScale;
//    private bool isPlayerAlive = true;

//    // Velocity before paused
//    private Vector2 previousVelocity;

//    // Attack delay
//    private float timerCount;
//    private bool isTimerOn = false;

//    private Vector2 forceTowardsPlayer;
//    private Vector2 direction;
//    private float distance;
//    [SerializeField] private float MIN_FORCE = 1;

//    [SerializeField] private GameObject healthPickup;
//    [SerializeField] private int healthPickupSpawnRate;

//    [SerializeField] private bool _isPaused = false;
//    public bool IsPaused
//    {
//        get { return _isPaused; }
//        private set { _isPaused = value; }
//    }

//    private void Awake()
//    {
//        StateManager.OnGameStateChanged += OnGameStateChangedHandler;
//        rb = GetComponent<Rigidbody2D>();
//        anim = GetComponent<Animator>();
//        health = GetComponent<HealthLogic>();
//        health.SetMaxHealth(properties.maxHealth);
//    }
//    private void OnDestroy() { StateManager.OnGameStateChanged -= OnGameStateChangedHandler; }

//    private void OnGameStateChangedHandler(GameStates state)
//    {
//        switch (state)
//        {
//            case GameStates.GamePause:
//                GamePauseHandler();
//                break;
//            case GameStates.GameResume:
//                GameResumeHandler();
//                break;
//            case GameStates.PlayerDeath:
//                PlayerDeathHandler();
//                break;
//        }
//    }
//    private void GamePauseHandler()
//    {
//        IsPaused = true;
//        rb.linearVelocity = Vector2.zero;
//        previousVelocity = rb.linearVelocity;
//    }
//    private void GameResumeHandler()
//    {
//        IsPaused = false;
//        rb.linearVelocity = previousVelocity;
//    }
//    private void PlayerDeathHandler()
//    {
//        isPlayerAlive = false;
//        DeathHandler();
//    }

//    private void Start()
//    {        
//        // Create Position Indicator
//        properties.positionIndicator = Instantiate(properties.positionIndicator).GetComponent<PositionIndicatorLogic>();
//        properties.positionIndicator.SetFollowTarget(this.gameObject);

//        do
//        {
//            player = GameObject.FindWithTag(TAG_PLAYER).GetComponent<PlayerController>();
//            playerGravityScale = player.GetGravityScale();
//        } while (!player);
//    }

//    private void Update()
//    {
//        if (isTimerOn)
//        {
//            timerCount -= Time.deltaTime;
//            if (timerCount <= 0f) { isTimerOn = false; }
//        }
//        else { PursuePlayer(); }
//    }

//    private void PursuePlayer()
//    {
//        direction = player.transform.position - transform.position;
//        distance = direction.magnitude;

//        forceTowardsPlayer = direction.normalized * ((playerGravityScale / (distance + 1f)) + MIN_FORCE);
//    }

//    private void FixedUpdate()
//    {
//        if (!IsPaused)
//        {
//            if (!isTimerOn)
//            {
//                rb.AddForce(forceTowardsPlayer);
//                rb.linearVelocity = Vector2.ClampMagnitude(rb.linearVelocity, properties.maxSpeed);
//            }
//        }
//    }

//    private void OnTriggerEnter2D(Collider2D collision)
//    {
//        if (!_isPaused)
//        {
//            if (collision.CompareTag(TAG_PLAYER))
//            {
//                if (collision.gameObject.TryGetComponent<PlayerController>(out PlayerController component))
//                {
//                    component.ApplyDamage(properties.damage);
//                    //AudioManager.instance.PlayPlayerSFX(properties.playerHitSFX);

//                    // Set and activate attack delay timer
//                    timerCount = properties.attackDelayTimer;
//                    isTimerOn = true;
//                }
//            }
//            else if (collision.CompareTag(TAG_PROJECTILE))
//            {
//                if (collision.gameObject.TryGetComponent<ProjectileLogic>(out ProjectileLogic projectile))
//                {
//                    //projectile.PlayHitSFX();
//                    health.ApplyDamage(projectile.GetDamage());
//                    Destroy(collision.gameObject);
//                }
//            }
//        }
//    }

//    void OnBecameInvisible() { if (properties.positionIndicator) { properties.positionIndicator.SetActive(true); } }
//    void OnBecameVisible() { if (properties.positionIndicator) { properties.positionIndicator.SetActive(false); } }

//    public void DeathHandler()
//    {
//        //Debug.Log("Asteroid Death Handler");
//        if (properties.positionIndicator.gameObject) { Destroy(properties.positionIndicator.gameObject); }
//        if (isPlayerAlive)
//        {
//            //AudioManager.instance.PlayEnemySFX(properties.deathSFX);
//            SpawnHealthPickup();
//        }
//        anim.SetBool("isAlive", false);

//        StateManager.instance.UpdateGameState(GameStates.EnemyDeath);
//    }

//    private void SpawnHealthPickup()
//    {
//        if (healthPickup != null)
//        {
//            int num = UnityEngine.Random.Range(0, 100);

//            if (num < healthPickupSpawnRate) { Instantiate(healthPickup, transform.position, transform.rotation); }
//        }
//    }
//}