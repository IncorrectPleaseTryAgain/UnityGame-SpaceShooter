using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class ProjectileLogic : MonoBehaviour
{
    [Serializable]
    private struct Properties
    {
        public string name;
        public string description;

        public float speed;
        public float lifetime; // Seconds
        public float damage;

        public List<AudioClip> shotSFX;
        public List<AudioClip> hitSFX;
    }
    [SerializeField] private Properties properties;
    [SerializeField] private Rigidbody2D rb;

    private void Awake() { rb = GetComponent<Rigidbody2D>(); }
    private void Start()
    {
        StartCoroutine(DestroyAfterSetTime());
    }

    private IEnumerator DestroyAfterSetTime()
    {
        yield return new WaitForSeconds(properties.lifetime);
        Destroy(gameObject);
    }

    public void Shoot(Vector2 velocity)
    {
        rb.linearVelocity = velocity;
        rb.linearVelocity += new Vector2(transform.up.x * properties.speed, transform.up.y * properties.speed);
        PlayShotSFX();
    }

    private void PlayShotSFX() { AudioManager.instance.PlayProjectileSFX(properties.shotSFX); }
    public float GetDamage() { return properties.damage; }
    public void PlayHitSFX() { AudioManager.instance.PlayProjectileSFX(properties.hitSFX); }
}
