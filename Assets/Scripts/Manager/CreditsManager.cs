using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CreditsManager : MonoBehaviour
{
    // Input
    public InputActionAsset InputActions;
    private InputAction cancelAction;
    private void OnEnable() { InputActions.FindActionMap("UI").Enable(); }
    private void OnDisable() { InputActions.FindActionMap("UI").Disable(); }

    // Credits
    private RectTransform creditsTransform;
    [SerializeField] private GameObject credits;


    // Properties
    [Serializable]
    private struct Properties
    {
        // Variables
        public float defaultSpeed;

        // Getters
        public float getDefaultSpeed() { return defaultSpeed; }
    };
    [SerializeField] private Properties properties;

    private float currentScrollSpeed;

    private void Awake()
    {
        creditsTransform = credits.GetComponent<RectTransform>();
        cancelAction = InputSystem.actions.FindAction("Cancel");
    }

    private void Start() { currentScrollSpeed = properties.getDefaultSpeed(); }

    void Update()
    {
        creditsTransform.position += Vector3.up * (Time.deltaTime * currentScrollSpeed);

        if (cancelAction.WasReleasedThisFrame()) { Cancel(); }
    }

    private void Cancel() { StateManager.instance.UpdateGameState(GameStates.SceneMainMenu); }
}
