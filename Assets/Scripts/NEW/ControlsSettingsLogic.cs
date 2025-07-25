using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class ControlsSettingsLogic : MonoBehaviour
{
    [SerializeField] private InputActionAsset _controls;

    [SerializeField] private Button BTN_Reset;
    private void OnDestroy()
    {
        BTN_Reset.onClick.RemoveListener(ResetControlsHandler);
    }
    private void Awake()
    {
        BTN_Reset.onClick.AddListener(ResetControlsHandler);
    }

    private void ResetControlsHandler()
    {
        foreach (InputActionMap map in _controls.actionMaps)
        {
            map.RemoveAllBindingOverrides();
        }
    }
    public void Load()
    {
        SaveSystem.Instance.LoadControls();
    }
    public void Save()
    {
        SaveSystem.Instance.SaveControls();
    }
}
