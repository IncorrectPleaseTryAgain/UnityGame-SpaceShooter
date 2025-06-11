using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Rendering.Universal;


public class BlackHoleLogic : MonoBehaviour
{
    private new Transform transform;
    private Dictionary<string, Rigidbody2D> rb = new Dictionary<string, Rigidbody2D>();
    private Dictionary<string, HealthLogic> health = new Dictionary<string, HealthLogic>();

    [SerializeField] private new Light2D light;
    [SerializeField] private float lightIntensityChangeSpeed;
    [SerializeField] private float maxLightVolumeIntensity;
    [SerializeField] private bool dimLight;
    [SerializeField] private bool brightenLight;

    [Serializable]
    private struct Properties
    {
        public float gravityScale;
        public float damage;
    }
    [SerializeField] Properties properties;

    private void Awake()
    {
        transform = gameObject.GetComponent<Transform>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Rigidbody2D rigidbody = collision.GetComponent<Rigidbody2D>();
        HealthLogic healthLogic = collision.GetComponent<HealthLogic>();
        // Execution order important
        if (healthLogic && rb.ContainsKey(collision.name) && !health.ContainsKey(collision.name)) { health.Add(collision.name, healthLogic); }
        if (rigidbody && !rb.ContainsKey(collision.name)) { rb.Add(collision.name, rigidbody); }

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        // Execution order important
        if (rb.ContainsKey(collision.name) && !health.ContainsKey(collision.name)) { rb.Remove(collision.name); }
        if (health.ContainsKey(collision.name)) { health.Remove(collision.name); }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        //Debug.Log(collision.name + " " + rb.ContainsKey(collision.name));
        if (rb.ContainsKey(collision.name))
        {
            // ex. direction vector from spaceship to black hole
            Vector2 dir = new((transform.position.x - collision.transform.position.x),
                               (transform.position.y - collision.transform.position.y));
            dir = dir.normalized;

            rb[collision.name].AddForce(dir * properties.gravityScale);

            if (health.ContainsKey(collision.name)) { health[collision.name].ApplyDamage(properties.damage * Time.deltaTime); }
        }
    }

    private void FixedUpdate()
    {
        if (brightenLight)
        {
            brightenLight = light.volumeIntensity < maxLightVolumeIntensity;
            light.volumeIntensity += lightIntensityChangeSpeed;
        }

        if (dimLight)
        {
            dimLight = light.volumeIntensity > 0;
            light.volumeIntensity -= lightIntensityChangeSpeed;
        }
    }

    public void AnimEventHandlerDeath() { Destroy(this.gameObject); }
    public void AnimEventHandlerDimLight() { dimLight = light; }
    public void AnimEventHandlerBrightenLight() { brightenLight = light; }
}
