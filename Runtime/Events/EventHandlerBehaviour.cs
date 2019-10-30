using UnityEngine;
using UnityEngine.Assertions;

namespace HouraiTeahouse {

/// <summary>
/// An abstract base class made for handling a specific type of event.
/// </summary>
/// <typeparam name="T"></typeparam>
public abstract class EventHandlerBehaviour<T> : MonoBehaviour {

    /// <summary>
    /// The Mediator to subscribe to. This can be overridden
    /// by subclasses. By default it uses the global mediator.
    /// </summary>
    protected virtual Mediator Mediator => Mediator.Global;

    /// <summary>
    /// The mediator context for the 
    /// </summary>
    protected MediatorContext Events { get; private set; }

    /// <summary>
    /// Unity Callback. Called on object instantiation.
    /// </summary>
    protected virtual void Awake() {
        Assert.IsNull(Events);
        Events = Mediator.CreateUnityContext(this);
        SubscribeEvents();
    }

    /// <summary>
    /// Unity Callback. Called when the behaviour is enabled.
    /// </summary>
    protected virtual void OnEnable() {
        if (Events != null) { UnsubscribeEvents(); }
        Events = Mediator.CreateUnityContext(this);
        SubscribeEvents();
    }

    /// <summary>
    /// Unity Callback. Called when the behaviour is disabled.
    /// </summary>
    protected virtual void OnDisable() => UnsubscribeEvents();

    /// <summary>
    /// Subscribes the behavior to events.
    /// </summary>
    protected virtual void SubscribeEvents() => Events?.Subscribe<T>(OnEvent);

    /// <summary>
    /// Unsubscribes the component from the events it's subscribed to.
    /// </summary>
    protected virtual void UnsubscribeEvents() => Events?.Dispose();

    /// <summary>
    /// The event handler callback.
    /// </summary>
    /// <param name="eventArgs">the event arguments</param>
    protected abstract void OnEvent(T eventArgs);

}

}