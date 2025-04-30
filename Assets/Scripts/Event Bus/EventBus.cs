using System;
using System.Collections.Generic;

public static class EventBus
{
    private static readonly Dictionary<Type, Delegate> _events = new();

    public static void Subscribe<T>(Action<T> callback)
    {
        var type = typeof(T);
        if (_events.TryGetValue(type, out var existing))
        {
            _events[type] = Delegate.Combine(existing, callback);
        }
        else
        {
            _events[type] = callback;
        }
    }

    public static void Unsubscribe<T>(Action<T> callback)
    {
        var type = typeof(T);
        if (_events.TryGetValue(type, out var existing))
        {
            _events[type] = Delegate.Remove(existing, callback);
            if (_events[type] == null)
                _events.Remove(type);
        }
    }

    public static void Publish<T>(T eventData)
    {
        var type = typeof(T);
        if (_events.TryGetValue(type, out var del))
        {
            ((Action<T>)del)?.Invoke(eventData); // If del can be cast to Action<T>, then call it with eventData; otherwise, do nothing
        }
    }
}
