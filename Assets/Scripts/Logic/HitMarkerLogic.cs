using UnityEngine;

public class HitMarkerLogic : MonoBehaviour
{
    [SerializeField] private float lifetime = .5f;
    [SerializeField] private Color hitColor = Color.red;
    [SerializeField] private int markerColorIntensity = 3;
    [SerializeField] private Material hitMarkerMaterial;

    private void Awake()
    {
        hitMarkerMaterial.SetColor("_Color", hitColor * markerColorIntensity);
    }

    private void FixedUpdate()
    {
        lifetime -= Time.fixedDeltaTime;
        hitMarkerMaterial.SetFloat("_Fade", lifetime / .5f);
        if (lifetime <= 0)
        {
            Destroy(gameObject);
        }
    }

}
