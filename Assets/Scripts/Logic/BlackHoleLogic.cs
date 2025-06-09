using System;
using UnityEngine;


public class BlackHoleLogic : MonoBehaviour
{
    private new Transform transform;
    private Rigidbody2D rb;

    [Serializable]
    private struct Properties
    {
        public float gravityScale;
    }
    [SerializeField] Properties properties;

    private void Awake()
    {
        transform = gameObject.GetComponent<Transform>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        rb = collision.GetComponent<Rigidbody2D>();
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (rb)
        {
            // ex. direction vector from spaceship to black hole
            Vector2 dir = new Vector2((transform.position.x - collision.transform.position.x),
                                                (transform.position.y - collision.transform.position.y));
            dir = dir.normalized;

            rb.AddForce(dir * properties.gravityScale);
        }
    }

    public void AnimEventHandlerDeath() { Destroy(this.gameObject); }
}
