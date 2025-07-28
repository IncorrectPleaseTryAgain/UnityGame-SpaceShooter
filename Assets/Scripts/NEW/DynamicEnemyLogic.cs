using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Collider2D))]
[RequireComponent(typeof(Animator))]
public class DynamicEnemyLogic : MonoBehaviour, IDamageable
{
    [Header("Components")]
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private new Collider2D collider;
    [SerializeField] private Animator animator;

    [Header("Dynamic Enemy")]
    [SerializeField] private DynamicEnemy dynamicEnemy;

    [SerializeField] private float health;
    private bool isAlive;

    private PlayerController player;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        collider = GetComponent<Collider2D>();
        animator = GetComponent<Animator>();

        spriteRenderer.sprite = dynamicEnemy.Sprite;
        animator.runtimeAnimatorController = dynamicEnemy.AnimatorController;

        health = dynamicEnemy.Health;
    }

    private void Start()
    {
        player = GameObject.FindWithTag("Player").GetComponent<PlayerController>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Check Collision");

        IWeapon weapon = collision.gameObject.GetComponent<IWeapon>();
        if (weapon != null)
        {
            Debug.Log("Apply Damage: " + weapon.Damage);
            ApplyDamage(weapon.Damage);
            Destroy(collision.gameObject);
        }
    }

    Vector3 direction;
    private void Update()
    {
        if (isAlive)
        {
            direction = (player.transform.position - transform.position).normalized;
            transform.position += direction * dynamicEnemy.MaxSpeed * Time.deltaTime;
        }
    }

    public void ApplyDamage(float damage)
    {
        AudioSystem.Instance.PlaySfx(dynamicEnemy.ACLP_Hit);
        health -= damage;
        Debug.Log("Health: " + health);
        if (health <= 0) { DeathHandler(); }
    }

    public void DeathHandler()
    {
        AudioSystem.Instance.PlaySfx(dynamicEnemy.ACLP_Death);
        isAlive = false;
        Destroy(gameObject);
    }
}
