using UnityEngine;
using UnityEngine.UI;

public class StaminaBarLogic : MonoBehaviour
{
    [SerializeField] private Gradient fillColour;
    [SerializeField] private Slider slider;

    public void SetMaxStamina(float health)
    {
        if (slider != null) { slider.maxValue = health; }
        SetStamina(health);
    }

    public void SetStamina(float health)
    {
        if (slider != null) { slider.value = health; }
        SetColour();
    }

    private void SetColour()
    {
        if (slider != null && slider.fillRect != null)
        {
            slider.fillRect.GetComponent<Image>().color = fillColour.Evaluate(slider.normalizedValue);
        }
    }
}
