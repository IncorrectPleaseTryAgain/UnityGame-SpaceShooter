using UnityEngine;

// <summary>
// A generic singleton class that can be used to create a static instance of a MonoBehaviour.
// It ensures that only one instance of the class exists and provides access to it.
// </summary>
public abstract class StaticInstance<T> : MonoBehaviour where T : MonoBehaviour
{
    public static T Instance { get; private set; }
    protected virtual void Awake() => Instance = this as T;

    protected virtual void OnApplicationQuit()
    {
        Instance = null;
        Destroy(gameObject);
    }

}

// <summary>
// A singleton class that inherits from StaticInstance and ensures that only one instance of the MonoBehaviour exists.
// If an instance already exists, it destroys the new instance to maintain the singleton pattern.
// </summary>
public abstract class Singleton<T> : StaticInstance<T> where T : MonoBehaviour
{
    protected override void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        base.Awake();
    }
}

// <summary>
// A persistent singleton class that inherits from StaticInstance and ensures that the instance persists across scene loads.
// It also ensures that only one instance of the MonoBehaviour exists.
// If an instance already exists, it destroys the new instance to maintain the singleton pattern.
// </summary>
public abstract class PersistentSingleton<T> : StaticInstance<T> where T : MonoBehaviour
{
    protected override void Awake()
    {
        base.Awake();
        DontDestroyOnLoad(gameObject);
    }
}
