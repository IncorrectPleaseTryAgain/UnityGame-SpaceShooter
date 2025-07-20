using System;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class CreditsManager : MonoBehaviour
{
    const string _logTag = "CreditsManager";

    [Header("Utility")]
    [SerializeField] PlayerInput _playerInput;

    [Header("Enviroment")]
    [SerializeField] Camera _camera;
    [SerializeField] GameObject _background;

    [Header("Credits")]
    [SerializeField] GameObject _creditsCanvas;
    [SerializeField] AudioClip _creditsMusic;
    [SerializeField] string _creditsTextObjName = "TMP Credits";
    TextMeshProUGUI creditsText;
    CreditsCanvasLogic creditsCanvasLogic;

    InputAction continueAction;
    InputAction navigateAction;


    private void Awake()
    {
        Initialize();
    }

    private void Start()
    {
        //AudioSystem.Instance.PlayMusic(_creditsMusic, true);

        TextMeshProUGUI[] TMProElements = _creditsCanvas.GetComponentsInChildren<TextMeshProUGUI>();
        foreach (TextMeshProUGUI element in TMProElements)
        {
            if (element.name == "TMP Credits")
            {
                creditsText = element;
                break;
            }
        }
        creditsText.text = Credits.GetCredits(GetChapterForCurrentSave());
    }

    public void Update()
    {
        if (continueAction.WasPressedThisFrame())
        {
            SceneSystem.Instance.LoadScene(Scenes.LevelSelect);
        }
        if (navigateAction.WasPressedThisFrame())
        {
            //LogSystem.Instance.Log("Continue action pressed", LogType.Debug, _logTag);
            creditsCanvasLogic.NavigationWasPressedHandler(navigateAction.ReadValue<Vector2>());
        }
        else if(navigateAction.WasReleasedThisFrame())
        {
            //LogSystem.Instance.Log("Continue action released", LogType.Debug, _logTag);
            creditsCanvasLogic.NavigationWasReleasedHandler();
        }
    }

    private void Initialize()
    {
        _playerInput.SwitchCurrentActionMap(ActionMap.GetActionMap(ActionMap.ActionMaps.Credits));
        _playerInput.enabled = true;
        navigateAction = _playerInput.actions["Navigate"];
        continueAction = _playerInput.actions["Continue"];

        Instantiate(_camera);
        Instantiate(_background);
        _creditsCanvas = Instantiate(_creditsCanvas);
        creditsCanvasLogic = _creditsCanvas.GetComponent<CreditsCanvasLogic>();
    }

    public void Continue(InputAction.CallbackContext context)
    {
        if(context.performed)
        {
            AudioSystem.Instance.StopMusic();
            SceneSystem.Instance.LoadScene(Scenes.MainMenu); // TODO: Change to LevelSelect in Specific Chapter
        }
    }

    Credits.Credit GetChapterForCurrentSave()
    {
        // Convert current chapter to Credit enum
        switch (SaveSystem.Instance.currentChapter)
        {
            case 1:
                return Credits.Credit.CHAPTER_1;
            case 2:
                return Credits.Credit.CHAPTER_2;
            case 3:
                return Credits.Credit.CHAPTER_3;
            case 4:
                return Credits.Credit.CHAPTER_4;
            case 5:
                return Credits.Credit.CHAPTER_5;
            default:
                return Credits.Credit.END; // Default to END if chapter is out of range
        }
    }
}
