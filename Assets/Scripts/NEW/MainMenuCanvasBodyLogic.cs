using System;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuCanvasBodyLogic : MonoBehaviour
{
    static readonly string _logTag = "MainMenuCanvasBodyLogic";

    [SerializeField] GameObject _createNewSaveOverlay;

    [SerializeField] GameObject saveLeft;
    [SerializeField] GameObject saveCenter;
    [SerializeField] GameObject saveRight;

    [Header("Settings")]
    [SerializeField] Button _settingsButton;

    [Header("Quit")]
    [SerializeField] Button _quitButton;


    [SerializeField] float _fadeInDuration;
    float elapsedTime = 0;
    bool fadeButtonsIn = false;

    static readonly Color colorTransparent = new Color(1f, 1f, 1f, 0f);
    static readonly Color gameSaveInactiveColor = new Color(1f, 1f, 1f, 0.25f);
    static readonly Color gameSaveActiveColor = new Color(1f, 1f, 1f, 1f);
    static readonly Color quitColor = new Color(1f, 0f, 0f, 1f);

    static readonly string NO_SAVE = "Empty";

    private void OnDestroy()
    {
        MainMenuCanvasHeaderLogic.OnContinueAnimationComplete -= Continue;
    }

    public void Initialize()
    {
        MainMenuCanvasHeaderLogic.OnContinueAnimationComplete += Continue;

        _createNewSaveOverlay.gameObject.SetActive(false);
        
        gameObject.SetActive(false);
    }

    private void Continue()
    {
        gameObject.SetActive(true);
        //fadeButtonsIn = true;
    }
    //private void Update()
    //{
    //    if (fadeButtonsIn)
    //    {
    //        elapsedTime += Time.deltaTime;
    //        if(elapsedTime >= _fadeInDuration)
    //        {
    //            elapsedTime = 0;
    //            fadeButtonsIn = false;
    //            SavesFadeIn(1f);
    //            return;
    //        }
    //        if (elapsedTime / _fadeInDuration > 0.25) { SetButtonsEnabled(true); }

    //        SavesFadeIn(elapsedTime / _fadeInDuration);
    //    }
    //}
    private void SavesFadeIn(float t)
    {
        //if (saveLeftActive)
        //{
        //    _saveLeftButton.image.color = Color.Lerp(colorTransparent, gameSaveActiveColor, t);
        //    _saveLeftText.color = Color.Lerp(colorTransparent, gameSaveActiveColor, t);
        //}
        //else
        //{
        //    _saveLeftButton.image.color = Color.Lerp(colorTransparent, gameSaveInactiveColor, t);
        //    _saveLeftText.color = Color.Lerp(colorTransparent, gameSaveInactiveColor, t);
        //}

        //if (saveCenterActive)
        //{
        //    _saveCenterButton.image.color = Color.Lerp(colorTransparent, gameSaveActiveColor, t);
        //    _saveCenterText.color = Color.Lerp(colorTransparent, gameSaveActiveColor, t);
        //}
        //else
        //{
        //    _saveCenterButton.image.color = Color.Lerp(colorTransparent, gameSaveInactiveColor, t);
        //    _saveCenterText.color = Color.Lerp(colorTransparent, gameSaveInactiveColor, t);
        //}

        //if (saveRightActive)
        //{
        //    _saveRightButton.image.color = Color.Lerp(colorTransparent, gameSaveActiveColor, t);
        //    _saveRightText.color = Color.Lerp(colorTransparent, gameSaveActiveColor, t);
        //}
        //else
        //{
        //    _saveRightButton.image.color = Color.Lerp(colorTransparent, gameSaveInactiveColor, t);
        //    _saveRightText.color = Color.Lerp(colorTransparent, gameSaveInactiveColor, t);
        //}

        _settingsButton.image.color = Color.Lerp(colorTransparent, new Color(1f, 1f, 1f, 1f), t);
        _quitButton.image.color = Color.Lerp(colorTransparent, quitColor, t);
    }

    // TODO: Refactor these to use a single method with parameters using button prefab script
}
