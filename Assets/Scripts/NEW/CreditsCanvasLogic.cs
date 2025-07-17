using TMPro;
using UnityEditor.XR;
using UnityEngine;

public class CreditsCanvasLogic : MonoBehaviour
{
    private const string _logTag = "CreditsCanvasLogic";
    [SerializeField] TextMeshProUGUI _creditsText;

    [SerializeField] float _creditsScrollSpeedMultiplier;
    [SerializeField] float _creditsScrollAcceleration;
    [SerializeField] float _creditsScrollMaxSpeed;
    private float _currentScrollSpeed;
    private Vector3 _scrollDirection;
    private bool _isManualScrolling;

    public void Awake()
    {
        _currentScrollSpeed = 0f;
        _scrollDirection = Vector3.up; // Default scroll direction
    }

    public void Update()
    {
        // Update the scroll speed based on input
        if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W))
        {
            _currentScrollSpeed += _creditsScrollAcceleration * Time.deltaTime;
            _currentScrollSpeed = Mathf.Min(_currentScrollSpeed, _creditsScrollMaxSpeed);
        }
        else if (Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S))
        {
            _currentScrollSpeed -= _creditsScrollAcceleration * Time.deltaTime;
            _currentScrollSpeed = Mathf.Max(_currentScrollSpeed, -_creditsScrollMaxSpeed);
        }
        else
        {
            // Decelerate when no input is given
            _currentScrollSpeed = Mathf.MoveTowards(_currentScrollSpeed, 0f, _creditsScrollAcceleration * Time.deltaTime);
        }
        // Scroll the credits text
        _creditsText.rectTransform.localPosition += _scrollDirection * _currentScrollSpeed * Time.deltaTime;
    }
    public void SetCreditsText(string text)
    {
        _creditsText.text = text;
    }

    public void SetScrollDirection(Vector3 direction)
    {
        _scrollDirection = direction.normalized;
    }

    // Set if player is using keys to scroll the credits
    public void SetIsManualScrolling(bool value)
    {
        _isManualScrolling = value;
    }
}
