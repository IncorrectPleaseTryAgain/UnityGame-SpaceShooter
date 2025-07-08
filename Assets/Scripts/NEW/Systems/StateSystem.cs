using System;
using System.Collections;

public enum States
{
    // Player States
    PlayerDamage,
    PlayerDeath,

    // Enemy States
    EnemyDamage,
    EnemyDeath,
}

public class StateSystem : Singleton<StateSystem>, ISystem
{
    const string _LogTag = "StateSystem";
    public static event Action<GameStates> OnGameStateChanged;

    public static event Action OnSystemInitialized;

    public IEnumerator Initialize()
    {
        LogSystem.Instance.Log("Initializing StateSystem...", LogType.Info, _LogTag);

        if (StateSystem.Instance == null) { yield return null; }

        OnSystemInitialized?.Invoke();
    }

    public void UpdateGameState(GameStates newState)
    {
        switch (newState)
        {
            default:
                LogSystem.Instance.Log("Unhandled game state: " + newState, LogType.Error, _LogTag);
                break;
        }
        OnGameStateChanged?.Invoke(newState);
    }
}
