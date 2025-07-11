using UnityEngine;
using UnityEngine.InputSystem;
using System;
using System.Collections;

[RequireComponent(typeof(PlayerInput))]
public class InputSystem : Singleton<InputSystem>, ISystem
{
    const string _logTag = "InputSystem";

    [SerializeField] PlayerInput _playerInput;
    [SerializeField] InputActionAsset actions;

    public enum ActionMaps
    {
        InGame,
        UI,
        Continue
    }

    public static event Action OnSystemInitialized;
    // Initialize System
    public IEnumerator Initialize()
    {
        if(InputSystem.Instance == null) { yield return null; }

        _playerInput = GetComponent<PlayerInput>();

        OnSystemInitialized?.Invoke();
    }


    // Initialize Inputs
    public void InitializeAudio()
    {
        LoadControls();
    }

    public void LoadControls()
    {
        SaveSystem.Instance.LoadControls(actions);
    }

    public void SaveControls()
    {
        SaveSystem.Instance.SaveControls(actions);
    }

    public void SetActionMapActive(ActionMaps actionMap)
    {
        switch (actionMap)
        {
            case ActionMaps.InGame:
                _playerInput.SwitchCurrentActionMap("InGame");
                LogSystem.Instance.Log("Switched to InGame Action Map.", LogType.Warning, _logTag);
                break;
            case ActionMaps.UI:
                _playerInput.SwitchCurrentActionMap("UI");
                LogSystem.Instance.Log("Switched to UI Action Map.", LogType.Warning, _logTag);
                break;
            case ActionMaps.Continue:
                _playerInput.SwitchCurrentActionMap("Continue");
                LogSystem.Instance.Log("Switched to Continue Action Map.", LogType.Warning, _logTag);
                break;
        }
    }

    public InputAction GetAction(string actionName)
    {
        if (_playerInput.actions.FindAction(actionName) != null)
        {
            return _playerInput.actions[actionName];
        }
        else
        {
            LogSystem.Instance.Log($"Action '{actionName}' not found in Input Actions.", LogType.Error, _logTag);
            return null;
        }
    }

    public void SetInputActive(bool isActive)
    {
        if (_playerInput != null)
        {
            _playerInput.enabled = isActive;
            LogSystem.Instance.Log($"Input system is now {(isActive ? "active" : "inactive")}.", LogType.Info, _logTag);
        }
        else
        {
            LogSystem.Instance.Log("PlayerInput component is not assigned.", LogType.Error, _logTag);
        }
    }

}