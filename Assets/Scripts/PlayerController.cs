using System;
using UnityEngine;
using Unity.Cinemachine;
using UnityEngine.InputSystem;
using System.Collections.Generic;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(PlayerInput))]
[RequireComponent(typeof(HealthLogic))]
public class PlayerController : MonoBehaviour, IHealthResponder
{
    [Serializable]
    private struct Properties
    {
        public string name;
        public string description;

        public float maxHealth;
        public float maxSpeed;
        public float acceleration;
        public float gravityScale;

        public List<AudioClip> deathSFX;

        public GameObject pauseMenu;
        public GameObject deathScreen;

        public AudioClip pauseMenuOpen;
        public AudioClip pauseMenuClose;
    }
    [SerializeField] private Properties properties;

    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private HealthLogic health;
    [SerializeField] private Animator anim;
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private GameObject projectileSpawner;
    [SerializeField] private CinemachineImpulseSource impulseSource;

    

    private Vector2 moveInput;

    private InputAction pauseAction;
    private InputAction moveAction;
    private PlayerInput input;

    [SerializeField] private bool _isMoving = false;
    public bool IsMoving
    {
        get { return _isMoving; }
        private set
        {
            if (_isMoving != value)
            {
                _isMoving = value;
            }
        }
    }

    [SerializeField] private bool _isAlive = true;
    public bool IsAlive
    {
        get { return _isAlive; }
        private set
        {
            _isAlive = value;
            anim.SetBool("isAlive", value);
        }
    }

    [SerializeField] private bool _isPaused = false;
    public bool IsPaused
    {
        get { return _isPaused; }
        private set { _isPaused = value; }
    }

    private void Awake()
    {
        StateManager.OnGameStateChanged += OnGameStateChangedHandler;
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        input = GetComponent<PlayerInput>();
        health = GetComponent<HealthLogic>();
        health.SetMaxHealth(properties.maxHealth);
    }

    private void Start() 
    { 
        pauseAction = InputSystem.actions.FindAction("Escape");
        moveAction = InputSystem.actions.FindAction("Move");
    }

    private void OnGameStateChangedHandler(GameStates state)
    {
        //Debug.Log("PlayerController GSC: " + state);
        switch (state)
        {
            case GameStates.GamePause:
                GamePauseHandler();
                break;
            case GameStates.GameResume:
                GameResumeHandler();
                break;
            case GameStates.PlayerDeath:
                PlayerDeathHandler();
                break;
        }
    }
    private void GamePauseHandler()
    {
        IsPaused = true;
        if (!properties.pauseMenu.activeSelf) { properties.pauseMenu.SetActive(true); }
        if (properties.deathScreen.activeSelf) { properties.deathScreen.SetActive(false); }
        AudioManager.instance.PlaySFX(properties.pauseMenuOpen);
    }
    private void GameResumeHandler()
    {
        IsPaused = false;
        if (properties.pauseMenu.activeSelf) { properties.pauseMenu.SetActive(false); }
        if (properties.deathScreen.activeSelf) { properties.deathScreen.SetActive(false); }
        AudioManager.instance.PlaySFX(properties.pauseMenuClose);
    }
    private void PlayerDeathHandler()
    {
        AudioManager.instance.PlayPlayerSFX(properties.deathSFX);
        if (properties.pauseMenu.activeSelf) { properties.pauseMenu.SetActive(false); }
        if (!properties.deathScreen.activeSelf) { properties.deathScreen.SetActive(true); }
        input.actions.Disable();

        anim.SetBool("isAlive", IsAlive);
    }

    private void OnDestroy() { StateManager.OnGameStateChanged -= OnGameStateChangedHandler; }

    private void Update()
    {
        if (IsAlive)
        {
            RotateToMousePosition();
        }
    }
    private void FixedUpdate()
    {
        if (IsAlive && !IsPaused)
        {
            rb.linearVelocity = Vector2.ClampMagnitude(rb.linearVelocity + (moveInput * properties.acceleration), properties.maxSpeed);
        }
    }
    public void OnMove(InputAction.CallbackContext context)
    {
        if (!IsPaused && IsAlive)
        {
            moveInput = moveAction.ReadValue<Vector2>();
            IsMoving = moveInput != Vector2.zero;
        }
    }
    public void OnEscape(InputAction.CallbackContext context)
    {
        if (context.performed) { TogglePause(); }
    }
    private void RotateToMousePosition()
    {
        // Get mouse position
        Vector2 mousePosition = Mouse.current.position.ReadValue();
        mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);

        // Rotate spaceship towards mouse position
        Vector2 direction = new Vector2(mousePosition.x - transform.position.x, mousePosition.y - transform.position.y);
        transform.up = direction;
    }
    public void OnAttack(InputAction.CallbackContext context)
    {
        //Debug.Log("Attack");
        if (!IsPaused && IsAlive)
        {
            if (context.performed) 
            { 
                projectileSpawner.GetComponent<ProjectileSpawnerLogic>().SpawnProjectile(projectilePrefab, rb.linearVelocity);
            }
        }
    }
    public void ApplyDamage(float damage) 
    {
        if (!IsPaused && IsAlive)
        {
            health.ApplyDamage(damage);
            impulseSource.GenerateImpulseWithForce(1f);
            IsAlive = health.IsAlive();
            if (!IsAlive) { StateManager.instance.UpdateGameState(GameStates.PlayerDeath); }
        }
    }
    public float GetGravityScale() { return properties.gravityScale; }
    private void TogglePause()
    {
        //Debug.Log("Toggle Pause");
        IsPaused = !IsPaused;
        if (IsPaused) { StateManager.instance.UpdateGameState(GameStates.GamePause); }
        else { StateManager.instance.UpdateGameState(GameStates.GameResume); }
    }

    public void DeathHandler()
    {
        //Debug.Log("PlayerController OnDeath");
        IsAlive = false;
        StateManager.instance.UpdateGameState(GameStates.PlayerDeath);
    }
}
