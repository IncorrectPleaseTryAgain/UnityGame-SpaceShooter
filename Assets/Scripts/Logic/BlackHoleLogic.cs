using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Rendering.Universal;


public class BlackHoleLogic : MonoBehaviour
{
    private new Transform transform;

    [SerializeField] private Light2D innerLight;
    [SerializeField] private Light2D outerLight;
    [SerializeField] private float lightDimSpeed;
    [SerializeField] private float lightBrightenSpeed;
    [SerializeField] private float maxInnerLightVolumeIntensity;
    [SerializeField] private float maxOuterLightVolumeIntensity;
    [SerializeField] private bool dimLight;
    [SerializeField] private bool brightenLight;

    [SerializeField] private CustomTrigger outerTrigger;
    [SerializeField] private CustomTrigger innerTrigger;


    private Dictionary<string, Rigidbody2D> rb = new Dictionary<string, Rigidbody2D>();
    HealthLogic health = null;
    [SerializeField] bool applyDamage = false;

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

        outerTrigger.TriggerEnter += OuterTriggerEnterHandler;
        innerTrigger.TriggerEnter += InnerTriggerEnterHandler;
        innerTrigger.TriggerExit += InnerTriggerExitHandler;
        outerTrigger.TriggerExit += OuterTriggerExitHandler;
        outerTrigger.TriggerStay += OuterTriggerStayHandler;
        innerTrigger.TriggerStay += InnerTriggerStayHandler;
    }


    private void OuterTriggerEnterHandler(Collider2D collision)
    {
        if(!rb.ContainsKey(collision.name))
        {
            Rigidbody2D rigidbody = collision.GetComponent<Rigidbody2D>();
            if (rigidbody && !collision.CompareTag("Black Hole Resistant") && !collision.CompareTag("Projectile"))
            {
                rb.Add(collision.name, rigidbody);
            }
        }
    }
    private void InnerTriggerEnterHandler(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            if (!health)
            {
                health = collision.GetComponent<HealthLogic>();
                applyDamage = true;
            }
        }
    }

    private void InnerTriggerExitHandler(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            health = null;
            applyDamage = false;
        }
    }

    private void OuterTriggerExitHandler(Collider2D collision)
    {
       if (rb.ContainsKey(collision.name))
        {
            rb.Remove(collision.name);
        }
    }

    private void OuterTriggerStayHandler(Collider2D collision)
    {
        if (rb.ContainsKey(collision.name))
        {
            // ex. direction vector from spaceship to black hole
            Vector2 dir = transform.position - collision.transform.position;
            dir = dir.normalized;
            rb[collision.name].AddForce(dir * properties.gravityScale);
        }
    }

    private void InnerTriggerStayHandler(Collider2D collision)
    {
        if (applyDamage) { health.ApplyDamage(properties.damage * Time.deltaTime); }
    }

    private void FixedUpdate()
    {
        if (brightenLight)
        {

            brightenLight = innerLight.intensity < maxInnerLightVolumeIntensity || outerLight.intensity < maxOuterLightVolumeIntensity;
            innerLight.intensity += lightBrightenSpeed * Time.deltaTime;
            outerLight.intensity += lightBrightenSpeed * Time.deltaTime;

            innerLight.intensity = Mathf.Min(innerLight.intensity, maxInnerLightVolumeIntensity);
            outerLight.intensity = Mathf.Min(outerLight.intensity, maxOuterLightVolumeIntensity);
        }

        if (dimLight)
        {
            dimLight = innerLight.intensity > 0 || outerLight.intensity > 0;
            innerLight.intensity -= lightDimSpeed * Time.deltaTime;
            outerLight.intensity -= lightDimSpeed * Time.deltaTime;
        }
    }

    public void AnimEventHandlerDimLight() { dimLight = innerLight; }
    public void AnimEventHandlerBrightenLight() { brightenLight = innerLight; }
}
