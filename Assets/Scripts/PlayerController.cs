using System;
using UnityEngine;
using Unity.Cinemachine;
using UnityEngine.InputSystem;
using System.Collections.Generic;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Animator))]
public class PlayerController : MonoBehaviour
{
    [Serializable]
    private struct Properties
    {
        public string name;
        public string description;

        public float health;
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
    [SerializeField] private Animator anim;
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private GameObject projectileSpawner;
    [SerializeField] private CinemachineImpulseSource impulseSource;

    private Vector2 moveInput;

    private InputAction pauseAction;
    private InputAction moveAction;

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
    }

    private void Start() 
    { 
        pauseAction = InputSystem.actions.FindAction("Pause");
        moveAction = InputSystem.actions.FindAction("Move");
    }

    private void OnGameStateChangedHandler(GameStates state)
    {
        Debug.Log("PlayerController GSC: " + state);
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
        AudioManager.instance.PlaySoundQueue(properties.pauseMenuOpen);
    }
    private void GameResumeHandler()
    {
        IsPaused = false;
        if (properties.pauseMenu.activeSelf) { properties.pauseMenu.SetActive(false); }
        if (properties.deathScreen.activeSelf) { properties.deathScreen.SetActive(false); }
        AudioManager.instance.PlaySoundQueue(properties.pauseMenuClose);
    }
    private void PlayerDeathHandler()
    {
        AudioManager.instance.PlayPlayerSFX(properties.deathSFX);
        if (properties.pauseMenu.activeSelf) { properties.pauseMenu.SetActive(false); }
        if (!properties.deathScreen.activeSelf) { properties.deathScreen.SetActive(true); }

        anim.SetBool("isAlive", IsAlive);
    }

    private void OnDestroy() { StateManager.OnGameStateChanged -= OnGameStateChangedHandler; }

    private void Update()
    {
        if (pauseAction.WasReleasedThisFrame()) { TogglePause(); }

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
            properties.health -= damage;
            IsAlive = properties.health > 0;
            if (IsAlive)
            {
                impulseSource.GenerateImpulseWithForce(1f);
            }
            else 
            {
                StateManager.instance.UpdateGameState(GameStates.PlayerDeath);
            }
        }
    }
    public float GetGravityScale() { return properties.gravityScale; }
    private void TogglePause()
    {
        Debug.Log("Toggle Pause");
        IsPaused = !IsPaused;
        if (IsPaused) { StateManager.instance.UpdateGameState(GameStates.GamePause); }
        else { StateManager.instance.UpdateGameState(GameStates.GameResume); }
    }

}
