using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(Animator))]
public class PlayerController : MonoBehaviour, IDamageable
{
    Spaceship spaceship;

    [SerializeField] private SpriteRenderer _spriteRenderer;
    [SerializeField] private Animator _animator;

    public float health { get; set; }
    public float speed { get; set; }

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _animator = GetComponent<Animator>();
    }

    public void Initialize(Spaceship spaceship)
    {
        this.spaceship = spaceship;

        _spriteRenderer.sprite = spaceship.Sprite;
        _animator.runtimeAnimatorController = spaceship.AnimatorController;


        health = spaceship.MaxHealth;
        speed = 0;
    }



}
