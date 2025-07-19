using TMPro;
using UnityEditor.XR;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CreditsCanvasLogic : MonoBehaviour
{
    private const string _logTag = "CreditsCanvasLogic";
    [SerializeField] TextMeshProUGUI _creditsText;

    [SerializeField] float scrollSpeed;
    [SerializeField] float scrollSpeedMultiplier;
    [SerializeField] Vector3 scrollDirection;

    public void Awake()
    {
        scrollDirection = Vector3.up * scrollSpeed;
    }

    public void Update()
    {
        _creditsText.rectTransform.position += scrollDirection * Time.deltaTime;
    }

    public void SetCreditsText(string text)
    {
        _creditsText.text = text;
    }
    public void NavigationWasPressedHandler(Vector2 direction)
    {
        scrollDirection = new Vector3(0f, direction.normalized.y * (scrollSpeedMultiplier * scrollSpeed), 0f);
    }
    public void NavigationWasReleasedHandler()
    {
        scrollDirection = Vector3.up * scrollSpeed;
    }
}
