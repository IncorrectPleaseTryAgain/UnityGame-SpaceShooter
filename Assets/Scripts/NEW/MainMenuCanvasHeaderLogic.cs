using DG.Tweening;
using EasyTextEffects;
using System;
using TMPro;
using UnityEngine;

public class MainMenuCanvasHeaderLogic : MonoBehaviour, IAnimationEventsReceiver
{
    static readonly string _logTag = "MainMenuCanvasHeaderLogic";
    [SerializeField] private TextMeshProUGUI TMP_Title;
    [SerializeField] private TextMeshProUGUI TMP_Continue;

    [SerializeField] private AudioClip ACLP_FadeIn;

    [SerializeField] private Animation ANIM_Title;
    [SerializeField] private AnimationClip ANIMCLP_TitleFadeIn;
    [SerializeField] private AnimationClip ANIMCLP_TitleContinue;
    private static readonly string TITLE_FADE_IN_KEY = "title_fade_in";
    private static readonly string TITLE_CONTINUE_KEY = "title_continue";

    [SerializeField] private float titlePosAfterContinueTransition_Y;
    private float titleContinueAnimDuration;

    public static event Action OnFadeInAnimationComplete;
    public static event Action OnContinueAnimationComplete;

    private void OnDestroy()
    {
        MainMenuManager.OnContinue -= Continue;
    }
    private void Awake()
    {
        MainMenuManager.OnContinue += Continue;
        ANIM_Title.AddClip(ANIMCLP_TitleFadeIn, TITLE_FADE_IN_KEY);
        ANIM_Title.AddClip(ANIMCLP_TitleContinue, TITLE_CONTINUE_KEY);
        titleContinueAnimDuration = ANIMCLP_TitleContinue.length;

        TMP_Title.color = Color.clear;
        TMP_Continue.color = Color.clear;
    }
    private void Start()
    {
        ANIM_Title.Play(TITLE_FADE_IN_KEY);
    }
    public void OnAnimationEvent(string eventName)
    {
        switch (eventName)
        {
            case "PlayFadeInSFX":
                AudioSystem.Instance.PlaySfx(ACLP_FadeIn);
                break;
            case "PlayContinueTextEffects":
                TMP_Continue.GetComponent<TextEffect>().StartManualEffects();
                break;
            case "FadeInAnimationComplete":
                OnFadeInAnimationComplete?.Invoke();
                break;
            case "ContinueAnimationComplete":
                OnContinueAnimationComplete?.Invoke();
                break;
        }
    }
    private void Continue()
    {
        //Disable Continue
        TMP_Continue.gameObject.SetActive(false);

        // Transition Title
        ANIM_Title.Play(TITLE_CONTINUE_KEY);
        TMP_Title.transform.DOLocalMoveY(titlePosAfterContinueTransition_Y, titleContinueAnimDuration);
    }
}
