using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuCanvasBodyLogic : MonoBehaviour
{
    const string _logTag = "MainMenuCanvasBodyLogic";
    [Header("Game Saves")]
    [SerializeField] Button _gameSaveLeft;
    [SerializeField] TextMeshProUGUI _gameSaveLeftText;
    [SerializeField] Color _gameSaveLeftColor;
    [SerializeField] Color _gameSaveLeftTextColor;

    [SerializeField] Button _gameSaveCenter;
    [SerializeField] TextMeshProUGUI _gameSaveCenterText;
    [SerializeField] Color _gameSaveCenterColor;
    [SerializeField] Color _gameSaveCenterTextColor;

    [SerializeField] Button _gameSaveRight;
    [SerializeField] TextMeshProUGUI _gameSaveRightText;
    [SerializeField] Color _gameSaveRightColor;
    [SerializeField] Color _gameSaveRightTextColor;

    [Header("Settings")]
    [SerializeField] Button _settings;
    [SerializeField] Color _settingsColor;

    [Header("Quit")]
    [SerializeField] Button _quit;
    [SerializeField] Color _quitColor;

    [SerializeField] float _fadeInDuration;
    float elapsedTime;
    bool fadeInButtons;

    private void OnEnable()
    {
        MainMenuCanvasHeaderLogic.OnContinueAnimationComplete += Continue;
    }
    private void OnDisable()
    {
        MainMenuCanvasHeaderLogic.OnContinueAnimationComplete -= Continue;
    }

    // Setup
    private void Awake()
    {
        fadeInButtons = false;
        elapsedTime = 0;

        _gameSaveLeftColor = _gameSaveLeft.image.color;
        _gameSaveLeftTextColor = _gameSaveLeft.GetComponentInChildren<TextMeshProUGUI>().color;

        _gameSaveCenterColor = _gameSaveCenter.image.color;
        _gameSaveCenterTextColor = _gameSaveCenter.GetComponentInChildren<TextMeshProUGUI>().color;

        _gameSaveRightColor = _gameSaveRight.image.color;
        _gameSaveRightTextColor = _gameSaveRight.GetComponentInChildren<TextMeshProUGUI>().color;

        _settingsColor = _settings.image.color;
        _quitColor = _quit.image.color;
    }
    // Manipulation
    public void Initialize()
    {
        Disable();

        // Initialize Game Saves
    }
    public void Disable()
    { 
        // Hide
        SetButtonsColorTransparent();

        // Disable Buttons
        ButtonsEnabled(false);
    }
    void Continue()
    {
        fadeInButtons = true;
    }
    private void Update()
    {
        if (fadeInButtons)
        {
            elapsedTime += Time.deltaTime;
            if(elapsedTime >= _fadeInDuration)
            {
                elapsedTime = 0;
                fadeInButtons = false;
                ButtonsFadeIn(1f);
                return;
            }
            if (elapsedTime / _fadeInDuration > 0.25) { ButtonsEnabled(true); }

            ButtonsFadeIn(elapsedTime / _fadeInDuration);
        }
    }
    private void ButtonsFadeIn(float t)
    {
        _gameSaveLeft.image.color = Color.Lerp(new Color(_gameSaveLeftColor.r, _gameSaveLeftColor.g, _gameSaveLeftColor.b, 0f), 
                                        new Color(_gameSaveLeftColor.r, _gameSaveLeftColor.g, _gameSaveLeftColor.b, 1f), t);
        _gameSaveLeftText.color = Color.Lerp(new Color(_gameSaveLeftTextColor.r, _gameSaveLeftTextColor.g, _gameSaveLeftTextColor.b, 0f),
                                        new Color(_gameSaveLeftTextColor.r, _gameSaveLeftTextColor.g, _gameSaveLeftTextColor.b, 1f), t);

        _gameSaveCenter.image.color = Color.Lerp(new Color(_gameSaveCenterColor.r, _gameSaveCenterColor.g, _gameSaveCenterColor.b, 0f),
                                            new Color(_gameSaveCenterColor.r, _gameSaveCenterColor.g, _gameSaveCenterColor.b, 1f), t);
        _gameSaveCenterText.color = Color.Lerp(new Color(_gameSaveCenterTextColor.r, _gameSaveCenterTextColor.g, _gameSaveCenterTextColor.b, 0f),
                                            new Color(_gameSaveCenterTextColor.r, _gameSaveCenterTextColor.g, _gameSaveCenterTextColor.b, 1f), t);

        _gameSaveRight.image.color = Color.Lerp(new Color(_gameSaveRightColor.r, _gameSaveRightColor.g, _gameSaveRightColor.b, 0f),
                                            new Color(_gameSaveRightColor.r, _gameSaveRightColor.g, _gameSaveRightColor.b, 1f), t);
        _gameSaveRightText.color = Color.Lerp(new Color(_gameSaveRightTextColor.r, _gameSaveRightTextColor.g, _gameSaveRightTextColor.b, 0f),
                                            new Color(_gameSaveRightTextColor.r, _gameSaveRightTextColor.g, _gameSaveRightTextColor.b, 1f), t);

        _settings.image.color = Color.Lerp(new Color(_settingsColor.r, _settingsColor.g, _settingsColor.b, 0f),
                                        new Color(_settingsColor.r, _settingsColor.g, _settingsColor.b, 1f), t);
        _quit.image.color = Color.Lerp(new Color(_quitColor.r, _quitColor.g, _quitColor.b, 0f),
                                    new Color(_quitColor.r, _quitColor.g, _quitColor.b, 1f), t);
    }
    void SetButtonsColorTransparent()
    {
        _gameSaveLeft.image.color = new Color(_gameSaveLeftColor.r, _gameSaveLeftColor.g, _gameSaveLeftColor.b, 0f);
        _gameSaveLeftText.color = new Color(_gameSaveLeftTextColor.r, _gameSaveLeftTextColor.g, _gameSaveLeftTextColor.b, 0f);

        _gameSaveCenter.image.color = new Color(_gameSaveCenterColor.r, _gameSaveCenterColor.g, _gameSaveCenterColor.b, 0f);
        _gameSaveCenterText.color = new Color(_gameSaveCenterTextColor.r, _gameSaveCenterTextColor.g, _gameSaveCenterTextColor.b, 0f);

        _gameSaveRight.image.color = new Color(_gameSaveRightColor.r, _gameSaveRightColor.g, _gameSaveRightColor.b, 0f);
        _gameSaveRightText.color = new Color(_gameSaveRightTextColor.r, _gameSaveRightTextColor.g, _gameSaveRightTextColor.b, 0f);

        _settings.image.color = new Color(_settingsColor.r, _settingsColor.g, _settingsColor.b, 0f);
        _quit.image.color = new Color(_quitColor.r, _quitColor.g, _quitColor.b, 0f);
    }
    void ButtonsEnabled(bool enabled)
    {
        // Disable Buttons
        _gameSaveLeft.enabled = enabled;
        _gameSaveCenter.enabled = enabled;
        _gameSaveRight.enabled = enabled;
        _settings.enabled = enabled;
        _quit.enabled = enabled;
        _settings.enabled = enabled;
    }

    //public void Enable()
    //{
    //    // Show
    //    ResetButtonColors();

    //    // Enable Buttons
    //    ButtonsEnabled(true);
    //}
    //private void ResetButtonColors()
    //{
    //    _gameSaveLeft.image.color = _gameSaveLeftColor;
    //    _gameSaveLeftText.color = _gameSaveLeftTextColor;

    //    _gameSaveCenter.image.color = _gameSaveCenterColor;
    //    _gameSaveCenterText.color = _gameSaveCenterTextColor;

    //    _gameSaveRight.image.color = _gameSaveRightColor;
    //    _gameSaveRightText.color = _gameSaveRightTextColor;

    //    _settings.image.color = _settingsColor;
    //    _quit.image.color = _quitColor;
    //}

}
