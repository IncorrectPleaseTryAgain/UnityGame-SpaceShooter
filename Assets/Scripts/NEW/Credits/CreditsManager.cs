using UnityEngine;
using UnityEngine.InputSystem;

public class CreditsManager : MonoBehaviour
{
    private static readonly string _logTag = "CreditsManager";

    [Header("Utility")]
    [SerializeField] private PlayerInput playerInput;

    [Header("Enviroment")]
    [SerializeField] private new Camera camera;
    [SerializeField] private GameObject background;

    [Header("Credits")]
    [SerializeField] private CreditsCanvasLogic credits;
    [SerializeField] private AudioClip ACLP_CreditsAudio;

    private void Awake()
    {
        playerInput.enabled = true;

        camera = Instantiate(camera);
        Instantiate(background, camera.transform);
        credits = Instantiate(credits);
        AudioSystem.Instance.PlayMusic(ACLP_CreditsAudio, true);
    }

    public void OnContinueAction(InputAction.CallbackContext context)
    {
        if(!context.performed) { return; }
        AudioSystem.Instance.StopMusic();
        SceneSystem.Instance.LoadScene(Scenes.LevelSelect);
    }
    public void OnNavigateAction(InputAction.CallbackContext context)
    {
        credits.Navigate(context.ReadValue<Vector2>());
    }
}
