using TMPro;
using DG.Tweening;
using UnityEngine;
using EasyTextEffects;
using System;

public class MainMenuCanvasLogic : MonoBehaviour, IAnimationEventsReceiver
{
    const string _logTag = "MainMenuCanvasLogic";

    [SerializeField] GameObject _title;
    [SerializeField] GameObject _continue;
    [SerializeField] AudioClip _titleFadeInSFX;
    [SerializeField] AnimationClip _titleFadeInAnim;
    [SerializeField] AnimationClip _titleContinueAnim;
    [SerializeField] Animation _titleAnimation;
    [SerializeField] Canvas _body;

    [Tooltip("End position Y for the title after continue is pressed. ")]
    [SerializeField] float _endPosY;
    [Tooltip("Duration for the title to move to its end position after continue is pressed. " +
                "Ensure the duration is the same amount as the title continue animation duration")]
    [SerializeField] float _moveDuration;

    const string FADE_IN_ANIM_KEY = "FadeIn";
    const string CONTINUE_ANIM_KEY = "Continue";

    const float CONTINUE_FADE_DURATION = .1f; 
    const float CONTINUE_FADE_END_VALUE = 0f;

    public static event Action OnTitleAnimationFinished;

    private void Awake()
    {
        _titleAnimation.AddClip(_titleFadeInAnim, FADE_IN_ANIM_KEY);
        
        _titleAnimation.Play(FADE_IN_ANIM_KEY);

        _title.transform.localPosition = Vector3.zero;
    }

    public void OnAnimationEvent(string eventName)
    {
        LogSystem.Instance.Log($"Received animation event: {eventName}", LogType.Info, _logTag);
        switch (eventName)
        {
            case "PlayFadeInSFX":
                AnimEventPlayFadeInSFX();
                break;
            case "PlayContinueTextEffects":
                AnimEventPlayContinueTextEffects();
                break;
            default:
                LogSystem.Instance.Log($"Unknown animation event: {eventName}", LogType.Warning, _logTag);
                break;
        }
    }

    public void AnimEventPlayFadeInSFX()
    {
        AudioSystem.Instance.PlaySfx(_titleFadeInSFX);
    }
    public void AnimEventPlayContinueTextEffects()
    {
        _continue.GetComponent<TextEffect>().StartManualEffects();
        OnTitleAnimationFinished?.Invoke();
    }

    public void Continue()
    {
        LogSystem.Instance.Log("Continue button pressed, transitioning to main menu.", LogType.Info, _logTag);

        // Remove continue text
        _continue.SetActive(false);

        // Animate title
        _titleAnimation.AddClip(_titleContinueAnim, CONTINUE_ANIM_KEY);
        _titleAnimation.Play(CONTINUE_ANIM_KEY);
        _title.transform.DOLocalMoveY(_endPosY, _moveDuration).SetEase(Ease.InOutQuad);
    }

}
