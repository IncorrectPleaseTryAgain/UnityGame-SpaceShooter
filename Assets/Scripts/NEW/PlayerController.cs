using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Splines;

[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(CircleCollider2D))]
[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour, IDamageable
{
    private const string _logTag = "PlayerController";
    private Spaceship spaceship;

    [SerializeField] private SpriteRenderer _spriteRenderer;
    [SerializeField] private Animator _animator;
    [SerializeField] private Rigidbody2D _rb;
    [SerializeField] private CircleCollider2D _circleCollider;

    public bool _IsMoving { get; set; }
    public bool _IsAttacking { get; set; }
    public bool _IsAlive { get; set; }
    public float _MaxHealth { get; set; }
    public float _Health { get; set; }
    public bool _IsSprinting { get; private set;}
    public Vector2 _Direction { get; private set; }
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
    private InputAction attackAction;

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _animator = GetComponent<Animator>();
        _rb = GetComponent<Rigidbody2D>();
        _circleCollider = GetComponent<CircleCollider2D>();

        _IsAlive = false;
        _IsMoving = false;
        _IsAttacking = false;
        _IsSprinting = false;
        _Direction = Vector2.up;

        _MaxHealth = GameDataSystem.currentSave.maxHp;
    }

    private void Update()
    {
        if (_IsAlive)
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
            LogSystem.Instance.Log($"Moving: {moveAction.ReadValue<Vector2>()}", LogType.Debug, _logTag);
            _IsMoving = true;
            _Direction = moveAction.ReadValue<Vector2>();
        }

        if (moveAction.WasReleasedThisFrame())
        {
            LogSystem.Instance.Log($"Moving: {moveAction.ReadValue<Vector2>()}", LogType.Debug, _logTag);
            _IsMoving = false;
            _Direction = Vector2.zero;
        }
    }

    private void HandleAttacking()
    {
        if (attackAction.IsPressed())
        {
            if (spaceship.weapon.Automatic && attackDelayTimer <= 0)
            {
                //attackDelayTimer = spaceship.weapon.ReloadDelay;
                Attack();
            }
            else if (!_IsAttacking)
            {
                Attack();
            }
            _IsAttacking = true;
        }
    }

    private void FixedUpdate()
    {
        if (_IsAlive && _IsMoving)
        {
            _rb.linearVelocity = Vector2.ClampMagnitude(_rb.linearVelocity + (_Direction * spaceship.Acceleration), spaceship.MaxSpeed);
        }
    }

    public void Initialize(Spaceship spaceship, PlayerInput playerInput)
    {
        this.spaceship = spaceship;

        moveAction = playerInput.actions["Move"];
        attackAction = playerInput.actions["Attack"];

        _spriteRenderer.sprite = spaceship.Sprite;
        _animator.runtimeAnimatorController = spaceship.AnimatorController;

        _MaxHealth = spaceship.MaxHealth;
        _Health = spaceship.MaxHealth;

        _IsAlive = true;
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

    public void Attack()
    {
        LogSystem.Instance.Log("Attacking...");
        
    }

    public void DeathHandler()
    {
        LogSystem.Instance.Log("Death Handler", LogType.Todo, _logTag);
    }
}
