using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuCanvasBodyLogic : MonoBehaviour
{
    const string _logTag = "MainMenuCanvasBodyLogic";
    [Header("Game Saves")]
    [SerializeField] GameObject _newGameOverlay;

    [SerializeField] Button _gameSaveLeft;
    [SerializeField] TextMeshProUGUI _gameSaveLeftText;
    [SerializeField] Button _deleteLeftSave;

    [SerializeField] Button _gameSaveCenter;
    [SerializeField] TextMeshProUGUI _gameSaveCenterText;
    [SerializeField] Button _deleteCenterSave;

    [SerializeField] Button _gameSaveRight;
    [SerializeField] TextMeshProUGUI _gameSaveRightText;
    [SerializeField] Button _deleteRightSave;

    [Header("Settings")]
    [SerializeField] Button _settings;

    [Header("Quit")]
    [SerializeField] Button _quit;

    [SerializeField] float _fadeInDuration;
    float elapsedTime;
    bool fadeInButtons;

    Color colorTransparent = new Color(1f, 1f, 1f, 0f);
    Color gameSaveInactive = new Color(1f, 1f, 1f, 0.25f);
    Color gameSaveActive = new Color(1f, 1f, 1f, 1f);
    Color colorQuit = new Color(1f, 0f, 0f, 1f);

    private void OnEnable()
    {
    }
    private void OnDestroy()
    {
        MainMenuCanvasHeaderLogic.OnContinueAnimationComplete -= Continue;
    }

    // Setup
    private void Awake()
    {
        MainMenuCanvasHeaderLogic.OnContinueAnimationComplete += Continue;
        fadeInButtons = false;
        elapsedTime = 0;
    }
    public void Initialize()
    {
        InitializeSaves();
        Disable();
    }

    private void InitializeSaves()
    {
        LogSystem.Instance.Log("Initializing Saves", LogType.Info, _logTag);
        _gameSaveLeftText.text = DataSystem.Instance.gameData.Save1Name;
        _deleteLeftSave.gameObject.SetActive(false);

        _gameSaveCenterText.text = DataSystem.Instance.gameData.Save2Name;
        _deleteCenterSave.gameObject.SetActive(false);

        _gameSaveRightText.text = DataSystem.Instance.gameData.Save3Name;
        _deleteRightSave.gameObject.SetActive(false);
    }

    public void UpdateGameSaves()
    {
        // Update Save Names
        _gameSaveLeftText.text = DataSystem.Instance.gameData.Save1Name;
        _gameSaveCenterText.text = DataSystem.Instance.gameData.Save2Name;
        _gameSaveRightText.text = DataSystem.Instance.gameData.Save3Name;

        // Update Save Colors
        _gameSaveLeft.image.color = DataSystem.Instance.gameData.Save1Active ? gameSaveActive : gameSaveInactive;
        _gameSaveLeftText.color = DataSystem.Instance.gameData.Save1Active ? gameSaveActive : gameSaveInactive;

        _gameSaveCenter.image.color = DataSystem.Instance.gameData.Save2Active ? gameSaveActive : gameSaveInactive;
        _gameSaveCenterText.color = DataSystem.Instance.gameData.Save2Active ? gameSaveActive : gameSaveInactive;

        _gameSaveRight.image.color = DataSystem.Instance.gameData.Save3Active ? gameSaveActive : gameSaveInactive;
        _gameSaveRightText.color = DataSystem.Instance.gameData.Save3Active ? gameSaveActive : gameSaveInactive;
    }

    public void Disable()
    { 
        // Hide
        SetButtonsColorTransparent();

        // Disable Buttons
        ButtonsEnabled(false);

        _newGameOverlay.gameObject.SetActive(false);
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
        if(DataSystem.Instance.gameData.Save1Active)
        {
            _gameSaveLeft.image.color = Color.Lerp(colorTransparent, gameSaveActive, t);
            _gameSaveLeftText.color = Color.Lerp(colorTransparent, gameSaveActive, t);
        }
        else
        {
            _gameSaveLeft.image.color = Color.Lerp(colorTransparent, gameSaveInactive, t);
            _gameSaveLeftText.color = Color.Lerp(colorTransparent, gameSaveInactive, t);
        }
        if(DataSystem.Instance.gameData.Save2Active)
        {
            _gameSaveCenter.image.color = Color.Lerp(colorTransparent, gameSaveActive, t);
            _gameSaveCenterText.color = Color.Lerp(colorTransparent, gameSaveActive, t);
        }
        else
        {
            _gameSaveCenter.image.color = Color.Lerp(colorTransparent, gameSaveInactive, t);
            _gameSaveCenterText.color = Color.Lerp(colorTransparent, gameSaveInactive, t);
        }
        if(DataSystem.Instance.gameData.Save3Active)
        {
            _gameSaveRight.image.color = Color.Lerp(colorTransparent, gameSaveActive, t);
            _gameSaveRightText.color = Color.Lerp(colorTransparent, gameSaveActive, t);
        }
        else
        {
            _gameSaveRight.image.color = Color.Lerp(colorTransparent, gameSaveInactive, t);
            _gameSaveRightText.color = Color.Lerp(colorTransparent, gameSaveInactive, t);
        }

        _settings.image.color = Color.Lerp(colorTransparent, new Color(1f, 1f, 1f, 1f), t);
        _quit.image.color = Color.Lerp(colorTransparent, colorQuit, t);
    }


    void SetButtonsColorTransparent()
    {
        _gameSaveLeft.image.color = colorTransparent;
        _gameSaveLeftText.color = colorTransparent;
        _gameSaveCenter.image.color = colorTransparent;
        _gameSaveCenterText.color = colorTransparent;
        _gameSaveRight.image.color = colorTransparent;
        _gameSaveRightText.color = colorTransparent;
        _settings.image.color = colorTransparent;
        _quit.image.color = colorTransparent;
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




    // TODO: Refactor these to use a single method with parameters using button prefab script

    public void OnLeftSaveButtonPointerEnterHandler()
    {
        if (DataSystem.Instance.gameData.Save1Active)
        {
            _deleteLeftSave.gameObject.SetActive(true);
        }
    }
    public void OnCenterSaveButtonPointerEnterHandler()
    {
        if (DataSystem.Instance.gameData.Save2Active)
        {
            _deleteCenterSave.gameObject.SetActive(true);
        }
    }
    public void OnRightSaveButtonPointerEnterHandler()
    {
        if (DataSystem.Instance.gameData.Save3Active)
        {
            _deleteRightSave.gameObject.SetActive(true);
        }
    }

    public void OnLeftSaveButtonPointerExitHandler()
    {
        if (_deleteLeftSave.IsActive())
        {
            _deleteLeftSave.gameObject.SetActive(false);
        }
    }
    public void OnCenterSaveButtonPointerExitHandler()
    {
        if (_deleteCenterSave.IsActive())
        {
            _deleteCenterSave.gameObject.SetActive(false);
        }
    }
    public void OnRightSaveButtonPointerExitHandler()
    {
        if (_deleteRightSave.IsActive())
        {
            _deleteRightSave.gameObject.SetActive(false);
        }
    }
}
