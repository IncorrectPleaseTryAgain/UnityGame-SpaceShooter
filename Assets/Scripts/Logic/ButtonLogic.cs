using UnityEngine;

public class ButtonLogic : MonoBehaviour
{
    [SerializeField] private AudioClip pointerEnterSFX;
    [SerializeField] private AudioClip clickSFX;
    [SerializeField] private GameStates onClickState;
    public void PlayPointerEnterSFX() { AudioManager.instance.PlaySFX(pointerEnterSFX); }
    public void OnClickSetState() {
        AudioManager.instance.PlaySFX(clickSFX);
        StateManager.instance.UpdateGameState(onClickState); 
    }

}
