# HouraiCore

A core utilities library underpinning most Hourai Teahouse projects in Unity 3D.

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
## ArrayPool
A shared pool of arrays to lower GC pressure by reusing arrays. Arrays are rented from the pool and then returned to the pool when no longer in use. Arrays do not need to be returned to the pool to function properly, but this will lower overall performance by decreasing reuse.

```csharp
// Renting an array of a given size. Note: the array may be bigger 
// than the provided size.
Collider[] rentedArray = ArrayPool<Collider>.Shared.Rent(256);

// Work with the array.
var hitCount = Phyics.OverlapSphereNoAlloc(..., rentedArray, ...);

// Return the array to the pool.
ArrayPool<Collide>.Shared.Return(rentedArray);
```
