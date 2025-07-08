using UnityEngine;
using UnityEngine.UI;

public class ButtonGameSaveLogic : MonoBehaviour
{
    const string _logTag = "ButtonGameSaveLogic";

    bool isActiveSave = false;

    private void Awake()
    {
    }

    public void OnClickHandler()
    {
        if (isActiveSave)
        {
            EventLoadSaveHandler();
        }
        else
        {
            EventCreateNewSaveHandler();
        }
    }

    void EventLoadSaveHandler()
    {
        LogSystem.Instance.Log("Loading save...", LogType.Info, _logTag);
    }

    void EventCreateNewSaveHandler()
    {
        LogSystem.Instance.Log("Creating a new save...", LogType.Info, _logTag);
    }

    public void OnPointerEnterHandler()
    {
        LogSystem.Instance.Log("Mouse enter button area.", LogType.Info, _logTag);
    }

    public void OnPointerExitHandler()
    {
        LogSystem.Instance.Log("Mouse exit button area.", LogType.Info, _logTag);
    }
}
