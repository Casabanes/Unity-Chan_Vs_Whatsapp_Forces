using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : MonoBehaviour
{
    public delegate void EventReveiver(params object[] parameters);

    static Dictionary<EventType, EventReveiver> _events = new Dictionary<EventType, EventReveiver>();
    private void Start()
    {
        
    }
    public static void Suscribe(EventType eventType, EventReveiver listener)
    {
        if (!_events.ContainsKey(eventType))
        {
            _events.Add(eventType, listener);
        }
        else
        {
            _events[eventType] += listener;
        }
    }
    public static void UnSuscribe(EventType eventType, EventReveiver listener)
    {
        if (_events.ContainsKey(eventType))
        {
            _events[eventType] -= listener;
            if (_events[eventType] == null)
            {
                _events.Remove(eventType);
            }
        }
    }
    public static void Trigger(EventType eventType, params object[] parameters)
    {
        if (_events.ContainsKey(eventType))
        {
            _events[eventType](parameters);
        }
    }
    public static void Clear()
    {
        _events.Clear();
    }
    public enum EventType
    {
        GameOver,
        Win
    }
}
