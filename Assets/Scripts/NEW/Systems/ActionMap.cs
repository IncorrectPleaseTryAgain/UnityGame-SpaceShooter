using UnityEngine.InputSystem;

public static class ActionMap 
{

    public enum ActionMaps
    {
        MainMenu,
        Credits,
        InGame,
    }

    public static string GetActionMap(ActionMaps actionMap)
    {
        switch (actionMap)
        {
            case ActionMaps.MainMenu:
                return "MainMenu";
            case ActionMaps.Credits:
                return "Credits";
            case ActionMaps.InGame:
                return "InGame";
            default:
                return null;

        }
    }
}