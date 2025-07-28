using System;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuCanvasBodyLogic : MonoBehaviour
{
    static readonly string _logTag = "MainMenuCanvasBodyLogic";

    [Header("Saves")]
    [SerializeField] private SaveSlotLogic saveLeft;
    [SerializeField] private SaveSlotLogic saveCenter;
    [SerializeField] private SaveSlotLogic saveRight;

    [Header("Quit")]
    [SerializeField] private Button BTN_Quit;

    private void OnDestroy()
    {
        MainMenuCanvasHeaderLogic.OnContinueAnimationComplete -= Continue;
    }
    private void Awake()
    {
        MainMenuCanvasHeaderLogic.OnContinueAnimationComplete += Continue;
        gameObject.SetActive(false);

        saveLeft.LoadSave();
        saveCenter.LoadSave();
        saveRight.LoadSave();
    }
    private void Continue()
    {
        gameObject.SetActive(true);
    }
}
