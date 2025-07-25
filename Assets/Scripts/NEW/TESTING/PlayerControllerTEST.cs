using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(CircleCollider2D))]
[RequireComponent(typeof(Rigidbody2D))]
public class PlayerControllerTEST : MonoBehaviour
{
    private const string _logTag = "PlayerControllerTEST";

    [SerializeField] private SpriteRenderer _spriteRenderer;
    [SerializeField] private Animator _animator;
    [SerializeField] private Rigidbody2D _rb;
    [SerializeField] private CircleCollider2D _circleCollider;

    [SerializeField] private bool IsMoving = false;
    [SerializeField] private bool IsAlive = true;
    [SerializeField] private bool IsSprinting = false;
    [SerializeField] private Vector2 Direction = Vector2.zero;
    [SerializeField] private float MaxHealth = 100;
    [SerializeField] private float MaxSpeed;
    [SerializeField] private float Acceleration;
    [SerializeField] private float SprintMultiplier;
    [SerializeField] private float GravityScale;

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _animator = GetComponent<Animator>();
        _rb = GetComponent<Rigidbody2D>();
        _circleCollider = GetComponent<CircleCollider2D>();
    }

    private void Update()
    {
        if (IsAlive)
        {
            RotateToMousePosition();
        }
    }

    private void FixedUpdate()
    {
        if (IsAlive && IsMoving)
        {
            _rb.linearVelocity = Vector2.ClampMagnitude(_rb.linearVelocity + (Direction * Acceleration), MaxSpeed);
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

    public void OnMovehandler(InputAction.CallbackContext context)
    {
        IsMoving = true;
        Direction = context.ReadValue<Vector2>();
    }

    public void DeathHandler()
    {
        LogSystem.Instance.Log("Death Handler", LogType.Todo, _logTag);
    }
}
