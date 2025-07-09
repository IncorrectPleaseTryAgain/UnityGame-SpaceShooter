using DG.Tweening;
using EasyTextEffects;
using System;
using TMPro;
using UnityEngine;

public class MainMenuCanvasHeaderLogic : MonoBehaviour, IAnimationEventsReceiver
{
    const string _logTag = "MainMenuCanvasHeaderLogic";
    [SerializeField] TextMeshProUGUI _title;
    [SerializeField] TextMeshProUGUI _continue;

    [SerializeField] AudioClip _titleFadeInClip;

    [SerializeField] Animation _titleAnimation;
    [SerializeField] AnimationClip _titleFadeInAnim;
    [SerializeField] AnimationClip _titleContinueAnim;
    const string TITLE_FADE_IN_KEY = "title_fade_in";
    const string TITLE_CONTINUE_KEY = "title_continue";

    [SerializeField] float _titleFinalPosY;
    float titleTransitionTime;

    public static event Action OnFadeInAnimationComplete;
    public static event Action OnContinueAnimationComplete;
    public void OnAnimationEvent(string eventName)
    {
        switch (eventName)
        {
            case "PlayFadeInSFX":
                AudioSystem.Instance.PlaySfx(_titleFadeInClip);
                break;
            case "PlayContinueTextEffects":
                _continue.GetComponent<TextEffect>().StartManualEffects();
                break;
            case "FadeInAnimationComplete":
                OnFadeInAnimationComplete?.Invoke();
                break;
            case "ContinueAnimationComplete":
                Debug.Log("OnContinueAnimationComplete?.Invoke()");
                OnContinueAnimationComplete?.Invoke();
                break;
        }
    }

    // Setup
    private void Awake()
    {
        _titleAnimation = _title.GetComponent<Animation>();
        _titleAnimation.AddClip(_titleFadeInAnim, TITLE_FADE_IN_KEY);
        _titleAnimation.AddClip(_titleContinueAnim, TITLE_CONTINUE_KEY);
        titleTransitionTime = _titleContinueAnim.length;
    }
    // Manipulation
    public void Initialize()
    {
        _title.color = new Color(1f, 1f, 1f, 0f);
        _continue.color = new Color(1f, 1f, 1f, 0f);
    }
    public void Start()
    {
        _titleAnimation?.Play(TITLE_FADE_IN_KEY);
    }
    public void Continue()
    {
        // Disable Continue
        _continue.gameObject.SetActive(false);

        // Transition Title
        _titleAnimation?.Play(TITLE_CONTINUE_KEY);
        _title.transform.DOLocalMoveY(_titleFinalPosY, titleTransitionTime);
    }
}
