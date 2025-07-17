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
        navigateAction.ReadValue<Vector2>();
    }

    private void Initialize()
    {
        _playerInput.SwitchCurrentActionMap(ActionMap.GetActionMap(ActionMap.ActionMaps.Credits));
        navigateAction = _playerInput.actions["Navigate"];
        continueAction = _playerInput.actions["Continue"];
        _playerInput.enabled = true;


        Instantiate(_camera);
        Instantiate(_background);
        Instantiate(_creditsCanvas);
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
        int chapter = 1; // Default to chapter 1
        switch (SaveSystem.Instance.currentGameSave)
        {
            case (int)SaveSystem.SaveIndex.Save1:
                chapter = SaveSystem.Instance.playerData.Save1Chapter;
                break;
            case (int)SaveSystem.SaveIndex.Save2:
                chapter = SaveSystem.Instance.playerData.Save2Chapter;
                break;
            case (int)SaveSystem.SaveIndex.Save3:
                chapter = SaveSystem.Instance.playerData.Save3Chapter;
                break;
        }

        // Convert current chapter to Credit enum
        switch (chapter)
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
