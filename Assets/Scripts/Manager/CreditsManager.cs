using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CreditsManager : MonoBehaviour
{
    // Input
    public InputActionAsset InputActions;
    private InputAction escapeAction;
    private InputAction moveAction;

    // Credits
    private RectTransform creditsTransform;
    [SerializeField] private GameObject credits;


    // Properties
    [Serializable]
    private struct Properties
    {
        // Variables
        public float defaultSpeed;
        public float speedModifier;

        // Getters
        public float getDefaultSpeed() { return defaultSpeed; }
    };
    [SerializeField] private Properties properties;

    private float currentScrollSpeed;

    private void Awake()
    {
        creditsTransform = credits.GetComponent<RectTransform>();
        escapeAction = UnityEngine.InputSystem.InputSystem.actions.FindAction("Escape");
        moveAction = UnityEngine.InputSystem.InputSystem.actions.FindAction("Move");
    }

    private void Start() { currentScrollSpeed = properties.getDefaultSpeed(); }

    void Update()
    {
        creditsTransform.position += Vector3.up * (Time.deltaTime * currentScrollSpeed);

        if (escapeAction.WasReleasedThisFrame()) { Cancel(); }

        if(moveAction.WasPressedThisFrame()) { AddSpeedModifier(true); }
        if(moveAction.WasReleasedThisFrame()) { AddSpeedModifier(false); }
    }

    private void Cancel() { StateManager.instance.UpdateGameState(GameStates.SceneMainMenu); }

    private void AddSpeedModifier(bool add) 
    {
        if (add)
        {
            Vector2 dir = moveAction.ReadValue<Vector2>();
            currentScrollSpeed *= (dir.x > 0f || dir.y > 0f) ? properties.speedModifier : (-1f * properties.speedModifier);
        }
        else { currentScrollSpeed = properties.defaultSpeed; }
    }
}
