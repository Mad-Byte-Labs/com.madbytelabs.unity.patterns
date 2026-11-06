# Pattern contracts and lifecycle

All APIs are intended for the Unity main thread. Collections are not thread-safe. Required dependencies reject null; optional callbacks and optional Unity parents may be null. Callback exceptions propagate unless explicitly aggregated during pool disposal.

## Command

A command must implement a meaningful inverse in Undo. CommandHistory retains successful operations only. Undo and Redo return false for an empty history. A newly executed command discards the redo branch. Failed execution, undo, or redo leaves the history stacks unchanged, but the history cannot roll back partial side effects inside a command. Clear the history at session end.

## Flyweight

FlyweightCache creates and retains one value per key. The creation delegate must not reenter the same cache. Keys must follow Dictionary key constraints, including non-null reference keys. Keep cached data immutable and store instance-specific state elsewhere. Clear releases references without disposing values or destroying Unity objects. The cache owner manages those resources.

## Observer

Signal invokes listeners synchronously in subscription order. Dispose each subscription in its owner's OnDisable or OnDestroy. Subscription disposal is idempotent. Listener exceptions stop the current publication and propagate. Subscription changes affect subsequent publications; listeners already captured for the current publication can still run. Clear removes current listeners without disposing their tokens.

## Prototype

Prototype.Clone uses Unity Instantiate semantics, including shared references to external assets. It is not a general deep copy of an arbitrary C# object graph. The caller destroys Unity clones. Implement IPrototype<T> when a plain object needs an explicit custom cloning contract, and document which references are shared.

## Singleton

Put one concrete Singleton subclass on a dedicated root GameObject. Instance is null before that object's Awake and after destruction. Initialize dependencies explicitly; do not rely on Awake order across different GameObjects. Duplicate instances destroy their entire GameObject.

Override OnInitialized and OnReleased rather than replacing Awake and OnDestroy. Override PersistBetweenScenes to false for scene-scoped instances. The default persistent instance uses DontDestroyOnLoad and therefore requires a root GameObject. Instance never creates hidden objects. Use normal scene reload settings when relying on fresh initialization between Play sessions; domain and scene reload combinations have not been validated.

## State

StateMachine.Change calls Exit before Enter. Repeating the same state reference is a no-op. Change(null) exits the current state without entering another. Nested transitions during Enter or Exit throw. Transitions during Tick are supported.

Tick receives deltaTimeSeconds explicitly. The caller chooses scaled or unscaled time and supplies a meaningful nonnegative simulation step. Lifecycle callbacks should not throw: the machine does not roll back side effects; if Exit throws the old state remains current, while a failed Enter leaves the new state current.

## Event Queue

Enqueue adds a notification without dispatching it. Drain processes at most the initial queue count and the nonnegative maxEvents budget. Events added during dispatch wait for another call. Nested Drain calls throw.

An event is removed before publication. If a listener throws, that event is consumed, later listeners are not invoked for it, and remaining queued events stay pending. Call Drain from an explicitly owned update or simulation step. Clear releases pending event references without disposing them.

## Service Locator

ServiceLocator is instance-scoped. Keep it in an explicit composition root, register services under their intended interface types, and clear it at session end. Duplicate registrations throw. TryGet returns false and null for an unregistered contract; Get throws. Unregister removes a registration without disposing its service. Service ownership remains with the registering code. Prefer constructor injection when dependencies can be declared directly.

## Object Pool

ObjectPool leases plain reference objects or Unity component instances created by a factory. Rent must return a new or inactive object, never another active lease. Return each lease exactly once; foreign objects and repeated returns throw. Do not destroy leased Unity objects independently.

The capacity is the maximum retained inactive count, not an active lease limit. Overflow objects are passed to onDestroy. Supply onDestroy when pooling Unity objects; without it the pool only releases managed references.

Use onRent to activate objects and onReturn to reset position, velocity, timers, and subscriptions before deactivation. Keep prefab templates inactive when configuration must happen before OnEnable. PrefabFactory.Create can serve as the pool's creation delegate.

Callbacks must not reenter the pool. Failed rent or return callbacks remove the object and invoke onDestroy; cleanup exceptions propagate. Return all active leases before Dispose. Disposal with active leases throws and leaves the pool usable. Disposal attempts cleanup for every inactive object, then aggregates failures. Disposal is idempotent after the first completed cleanup attempt.

## Decorator

ConstantStat supplies an immutable base value. AddStat and MultiplyStat wrap an IStat; evaluation follows the nesting order. Adding five to ten and then multiplying by two produces thirty; multiplying ten by two and then adding five produces twenty-five. These examples use float for ordinary gameplay statistics, not exact currency or large-number economy calculations.

## Factory

DelegateFactory delegates construction to caller-provided code. PrefabFactory instantiates a required live Unity component prefab under an optional parent. The caller owns created instances and destroys or returns them to a pool. Factories do not track or release products.
