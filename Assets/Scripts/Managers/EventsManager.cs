using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

// The EventsManager class manages events and their listeners.
public class EventsManager: BaseSingletonMonoBehaviour<EventsManager>
{
    private Dictionary<EventType, EventListenersData> eventListenersData = new();
    // Adds listener for the specified event type, and adds action to be invoked when the event is triggered
    public void AddListener(EventType eventType, Action<object> methodToInvoke)
    {
        if (eventListenersData.TryGetValue(eventType, out var value))
        {
            value.ActionsOnInvoke.Add(methodToInvoke);
        }
        else
        {
            eventListenersData[eventType] = new EventListenersData(methodToInvoke);
        }
    }

    // Removes listener for the specified event type
    public void RemoveListener(EventType eventType, Action<object> methodToInvoke)
    {
        if (!eventListenersData.TryGetValue(eventType, out var value))
        {
            return;
        }

        value.ActionsOnInvoke.Remove(methodToInvoke);

        if (!value.ActionsOnInvoke.Any())
        {
            eventListenersData.Remove(eventType);
        }
    }

    // Invokes all listeners registered for the specified event type, and the data to pass to the listeners when invoking the event
    public void InvokeEvent(EventType eventType, object dataToInvoke = null)
    {
        if (!eventListenersData.TryGetValue(eventType, out var value))
        {
            return;
        }

        foreach (var method in value.ActionsOnInvoke)
        {
            method.Invoke(dataToInvoke);
        }
    }
}

// Represents the data of listeners for a specific event type.
public class EventListenersData
{
    public List<Action<object>> ActionsOnInvoke;

    public EventListenersData(Action<object> additionalData)
    {
        ActionsOnInvoke = new List<Action<object>>
        {
            additionalData
        };
    }
}

// Enum representing different event types.
public enum EventType
{
    OnStoryTriggerHit,
    OnPlayerLoss,
    OnInteractableChangeLockStateRequest,
    OnDialogueChange,
}