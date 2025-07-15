using UnityEngine;

public class ChapterSelectManager : MonoBehaviour
{
    public void GoToMainMenu() { SceneSystem.Instance.LoadScene(Scenes.MainMenu); }
}
