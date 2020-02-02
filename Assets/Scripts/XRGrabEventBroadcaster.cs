using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class XRGrabEventBroadcaster : MonoBehaviour
{
    [SerializeField]
    public UnityGameObjectEvent broadcastGameObjectEvent;
    public void RaiseEvent()
    {
        broadcastGameObjectEvent.Invoke(gameObject);
    }
}
