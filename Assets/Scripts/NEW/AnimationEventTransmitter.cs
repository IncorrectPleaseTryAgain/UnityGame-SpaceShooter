using UnityEngine;

public class AnimationEventTransmitter : MonoBehaviour
{
    const string _logTag = "AnimationEventTransmitter";
    public void TransmitEvent(string eventName)
    {
        IAnimationEventsReceiver receiver = GetComponentInParent<IAnimationEventsReceiver>();
        
        if (receiver != null)
        {
            //LogSystem.Instance.Log($"Transmitting Anim Event: '{eventName}'", LogType.Info, _logTag);
            receiver.OnAnimationEvent(eventName);
        }
        else
        {
            LogSystem.Instance.Log(
                $"No receiver found for animation event '{eventName}' on {gameObject.name}.",
                LogType.Warning,
                _logTag
            );
        }
    }
}
