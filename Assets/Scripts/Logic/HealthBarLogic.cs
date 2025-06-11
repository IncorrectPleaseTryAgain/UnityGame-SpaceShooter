using UnityEngine;
using UnityEngine.UI;

public class HealthBarLogic : MonoBehaviour
{
    [SerializeField] private Gradient fillColour;
    [SerializeField] private Slider slider;
    [SerializeField] private Image heart;
    [SerializeField] private Image heartBroken;

    public void SetMaxHealth(float health)
    {
        if (slider != null) { slider.maxValue = health; }
        SetHealth(health);
    }

    public void SetHealth(float health)
    {
        if (slider != null) { slider.value = health; }
        if (slider.value <= 0) { SetBrokenHeart(); }
        SetColour();
    }

    private void SetColour()
    {
        if (slider != null && slider.fillRect != null)
        {
            slider.fillRect.GetComponent<Image>().color = fillColour.Evaluate(slider.normalizedValue);
        }
    }

    private void SetBrokenHeart()
    {
        if (heart != null) { heart.enabled = false; }
        if (heartBroken != null) { heartBroken.enabled = true; }
    }

}
