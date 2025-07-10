using UnityEngine;
using System;
using System.Collections;
public enum LogType
{
    None,
    Info,
    Warning,
    Error,
    Todo
}

public class LogSystem : Singleton<LogSystem>, ISystem
{
    const string _logTag = "LogSystem";

    [SerializeField] bool _globalLogEnabled = true;
    [SerializeField] bool _InfoEnabled = true;
    [SerializeField] bool _WarningEnabled = true;
    [SerializeField] bool _ErrorEnabled = true;
    [SerializeField] bool _TodoEnabled = true;

    [SerializeField] Color infoColor = Color.black;
    [SerializeField] Color warningColor = Color.yellow;
    [SerializeField] Color errorColor = Color.red;
    [SerializeField] Color todoColor = Color.green;

    public static event Action OnSystemInitialized;
    public IEnumerator Initialize()
    {
        LogSystem.Instance.Log("Initializing LogSystem...", LogType.Info, _logTag);

        if(LogSystem.Instance == null) { yield return null; }

        OnSystemInitialized?.Invoke();
    }

    public void Log(string message, LogType type = LogType.Info, string _logTag = "N/A")
    {
        if (!_globalLogEnabled) return;
        switch (type)
        {
            case LogType.Info:
                if (!_InfoEnabled) return;
                Debug.Log($"<color=#{ColorUtility.ToHtmlStringRGB(infoColor)}>[{_logTag}]</color> {message}");
                break;
            case LogType.Warning:
                if (!_WarningEnabled) return;
                Debug.LogWarning($"<color=#{ColorUtility.ToHtmlStringRGB(warningColor)}>[{_logTag}]</color> {message}");
                break;
            case LogType.Error:
                if (!_ErrorEnabled) return;
                Debug.LogError($"<color=#{ColorUtility.ToHtmlStringRGB(errorColor)}>[{_logTag}]</color> {message}");
                break;
            case LogType.Todo:
                if (!_TodoEnabled) return;
                Debug.Log($"<color=#{ColorUtility.ToHtmlStringRGB(todoColor)}>[{_logTag}] TODO:</color> {message}");
                break;
            default:
                Log("Invalid log type specified.", LogType.Error, "LogSystem");
                break;
        }
    }

    public void DisableLog(LogType type)
    {
        switch(type)
        {
            case LogType.Info:
                _InfoEnabled = false;
                break;
            case LogType.Warning:
                _WarningEnabled = false;
                break;
            case LogType.Error:
                _ErrorEnabled = false;
                break;
            case LogType.Todo:
                _TodoEnabled = false;
                break;
            default:
                _globalLogEnabled = false;
                break;
        }
    }
}