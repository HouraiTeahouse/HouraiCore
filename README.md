# Hourai Teahouse Core Libraries for Unity

A core utilities library underpinning most Hourai Teahouse projects in Unity 3D.

## Standard Libraries
Unity does not include some key items for `System.*` binaries. This package includes
the following:

 * [System.Buffers](https://www.nuget.org/packages/System.Buffers/)
 * [System.Memory](https://www.nuget.org/packages/System.Memory/)

## Mediator
An event bus for subscribing to and publishing events.
```csharp
// Events are just data objects.
public class LogEvent {
  public string Message;
}

// Creating a Mediator.
Mediator mediator = new Mediator();

// Accessing the global event bus.
Mediator globalMediator = Mediator.Global;

// Subscribing to events.
Mediator.Global.Subscribe<LogEvent>(log => Debug.Log(log.Message));

// Publishing Events.
Mediator.Global.Publish(new LogEvent { Message = "Hello World!" });

// Unsubscribing from events.
Mediator.Global.Unsubscribe(...);
```
