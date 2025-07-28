using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(CapsuleCollider2D))]
public class PrimaryWeaponLogic : MonoBehaviour, IWeapon
{
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private CapsuleCollider2D capsuleCollider;

    [SerializeField] private PrimaryWeapon primaryWeapon;

    public float Damage {  get; set; }

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        capsuleCollider = GetComponent<CapsuleCollider2D>();
    }

    public void Initialize(PrimaryWeapon primaryWeapon)
    {
        this.primaryWeapon = primaryWeapon;

        spriteRenderer.sprite = primaryWeapon.Sprite;

        Damage = primaryWeapon.Damage * GameDataSystem.currentSave.primaryDamageMultiplier;
    }


    private void Start()
    {
        AudioSystem.Instance.PlaySfx(primaryWeapon.ACLP_Fire);
        StartCoroutine(DestroyAfterSetTime());
    }

    private IEnumerator DestroyAfterSetTime()
    {
        yield return new WaitForSeconds(primaryWeapon.Lifetime);
        Destroy(gameObject);
    }

    public void Fire(Vector2 vel)
    {
        rb.linearVelocity = vel;
    }

    private void Update()
    {
        rb.linearVelocity += new Vector2(transform.up.x * primaryWeapon.Speed, transform.up.y * primaryWeapon.Speed);
    }
}
