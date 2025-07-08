using System;

public static class InitializationManager
{
    static int TOTAL_SYSTEMS = 10;
    static int systemsReady = 0;

    public static event Action AllSystemsReady;
    public static void RegisterInitialized()
    {
        systemsReady++;
        if (systemsReady >= TOTAL_SYSTEMS)
        {
            AllSystemsReady?.Invoke();
        }
    }
}