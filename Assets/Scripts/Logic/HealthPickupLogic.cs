using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;

[RequireComponent(typeof(Rigidbody2D))]
public class HealthPickupLogic : MonoBehaviour
{
    [SerializeField] private float lifetime = 60;
    [SerializeField] private float minHealthAmount = 1.0f;
    [SerializeField] private float maxHealthAmount = 20.0f;
    [SerializeField] private float colourIntensity = 7.0f;
    [SerializeField] private Material pickupMaterial;
    [SerializeField] private float maxSpeed;
    [SerializeField] private float force;
    [SerializeField] private Rigidbody2D rb;

    private float currentLifetime = 0.0f;

    private void Awake()
    {
        float factor = Mathf.Pow(2, colourIntensity);
        pickupMaterial.SetColor("_MainText", new Color(Random.value * factor, Random.value * factor, Random.value * factor, 1.0f));
        if (!rb) { rb = GetComponent<Rigidbody2D>(); }
    }

    private void Start()
    {
        currentLifetime = lifetime;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Vector2 dir = collision.transform.position - transform.position;
            dir = dir.normalized;

            rb.AddForce(dir * force);
            rb.linearVelocity = Vector2.ClampMagnitude(rb.linearVelocity, maxSpeed);
        }
    }

    private void FixedUpdate()
    {
        currentLifetime -= Time.fixedDeltaTime;
        pickupMaterial.SetFloat("_Alpha", currentLifetime / lifetime); 
        if (currentLifetime <= 0)
        {
            Destroy(gameObject);
        }
    }

    public float GetHealthAmount()
    {
        return Random.Range(minHealthAmount, maxHealthAmount);
    }
}
