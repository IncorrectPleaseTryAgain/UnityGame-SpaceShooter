using System;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(CircleCollider2D))]
[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour, IDamageable, IRegenerative
{
    private const string _logTag = "PlayerController";
    private Spaceship spaceship;

    [Header("Components")]
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private Animator animator;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private PrimaryWeaponLogic primaryWeapon;

    public bool IsAlive { get; set; }

    public bool IsMoving { get; private set; }
    public bool IsAttacking { get; private set; }
    public bool IsSprinting { get; private set;}

    private float maxHealth;
    private float health;
    private Vector2 moveDirection;
    /*
    "shieldUnlocked": false,
    "secondaryUnlocked": false,
    "boostUnlocked": false,
    "autoFireUnlocked": false,
    "maxHp": 100.0,
    "hpRegenDelay": 5.0,
    "hpRegenSpeed": 10.0,
    "primaryAmmo": 10,
    "primaryDamage": 10.0,
    "primaryAmmoRegenSpeed": 10.0,
    "primaryReloadSpeed": 2.0,
    "secondaryAmmo": 10,
    "secondaryDamage": 10.0,
    "secondaryAmmoRegenSpeed": 10.0,
    "secondaryReloadSpeed": 2.0,
    "boostDuration": 0.5,
    "boostDelay": 2.0,
    "shieldDurability": 20.0,
    "shieldRegenDelay": 5.0
    */

    private float attackDelayTimer = 0;

    private InputAction moveAction;
    private InputAction primaryAttackAction;
    private InputAction secondaryAttackAction;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        //circleCollider = GetComponent<CircleCollider2D>();

        IsAlive = false;
        IsMoving = false;
        IsAttacking = false;
        IsSprinting = false;
        moveDirection = Vector2.up;
    }

    private void Update()
    {
        if (IsAlive)
        {
            RotateToMousePosition();
            HandleAttacking();
            HandleMoving();
        }
    }

    private void HandleMoving()
    {
        if (moveAction.IsPressed())
        {
            IsMoving = true;
            moveDirection = moveAction.ReadValue<Vector2>();
        }

        if (moveAction.WasReleasedThisFrame())
        {
            IsMoving = false;
            moveDirection = Vector2.zero;
        }
    }

    private void HandleAttacking()
    {
        // Primary
        if (primaryAttackAction.WasPressedThisFrame())
        {
            Instantiate(primaryWeapon, transform.position, transform.rotation).Fire(rb.linearVelocity);
        }

        // Secondary
        //if(secondaryAttackAction.WasReleasedThisFrame())
        //{

        //}
    }

    private void FixedUpdate()
    {
        if (IsAlive && IsMoving)
        {
            rb.linearVelocity = Vector2.ClampMagnitude(rb.linearVelocity + (moveDirection * spaceship.Acceleration), spaceship.MaxSpeed);
        }
    }

    public void Initialize(Spaceship spaceship, PlayerInput playerInput)
    {
        this.spaceship = spaceship;

        moveAction = playerInput.actions["Move"];
        primaryAttackAction = playerInput.actions["Primary Attack"];
        secondaryAttackAction = playerInput.actions["Secondary Attack"];

        spriteRenderer.sprite = spaceship.Idle;
        animator.runtimeAnimatorController = spaceship.AnimatorController;

        primaryWeapon.Initialize(spaceship.PrimaryWeapon);

        maxHealth = spaceship.Health * GameDataSystem.currentSave.maxHpMultiplier;
        health = maxHealth;

        IsAlive = true;
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

    public void ApplyDamage(float damage)
    {
        AudioSystem.Instance.PlaySfx(spaceship.ACLP_Hit);
        Math.Clamp(health -= health, 0, maxHealth);
        if (health <= 0) { DeathHandler(); }
    }

    public void ApplyHealth(float health)
    {
        Math.Clamp(health += health, 0, maxHealth);
    }

    public void DeathHandler()
    {
        LogSystem.Instance.Log("Death Handler", LogType.Todo, _logTag);
    }
}
