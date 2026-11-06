using MadByteLabs.Patterns.Samples.Command;
using MadByteLabs.Patterns.Samples.ServiceLocator;
using MadByteLabs.Patterns.Samples.Singleton;
using MadByteLabs.Patterns.Samples.State;
using MadByteLabs.Patterns;
using UnityEngine;

namespace MadByteLabs.Patterns.Samples
{
    /// <summary>
    /// Provides patterns demo behavior for the pattern demonstration or tests.
    /// </summary>
    public sealed class PatternsDemo : MonoBehaviour
    {
        /// <summary>
        /// Demonstrates all eleven retained patterns once when the component starts.
        /// </summary>
        private void Start()
        {
            var history = new CommandHistory();
            var command = new CounterCommand();
            history.Execute(command);
            history.Undo();
            history.Redo();
            Debug.Log("Command: " + command.Value, this);
            var cache = new FlyweightCache<string, string>(key => key.ToUpperInvariant());
            Debug.Log("Flyweight: " + cache.Get("shared"), this);
            var signal = new Signal<string>();
            using (signal.Subscribe(value => Debug.Log("Observer: " + value, this)))
            {
                signal.Publish("hello");
            }

            var source = new GameObject("Prototype source");
            var clone = MadByteLabs.Patterns.Prototype.Clone(source);
            Debug.Log("Prototype: " + clone.name, this);
            Destroy(source);
            Destroy(clone);
            var session = new GameObject("Demo singleton").AddComponent<DemoSession>();
            Debug.Log("Singleton: " + (DemoSession.Instance == session), this);
            var states = new StateMachine();
            states.Change(new IdleState());
            states.Tick(0f);
            states.Change(null);
            var queue = new EventQueue<string>();
            using (queue.Dispatched.Subscribe(value => Debug.Log("Event Queue: " + value, this)))
            {
                queue.Enqueue("deferred");
                queue.Drain();
            }

            var services = new MadByteLabs.Patterns.ServiceLocator();
            services.Register<IMessageService>(new MessageService());
            services.Get<IMessageService>().Write("Service Locator: resolved");
            services.Clear();
            using (var pool = new ObjectPool<object>(() => new object()))
            {
                object item = pool.Rent();
                pool.Return(item);
                Debug.Log("Object Pool: " + pool.CountInactive, this);
            }

            IStat stat = new MultiplyStat(new AddStat(new ConstantStat(10f), 5f), 2f);
            Debug.Log("Decorator: " + stat.Value, this);
            IFactory<object> factory = new DelegateFactory<object>(() => new object());
            object product = factory.Create();
            Debug.Log("Factory: " + product.GetType().Name, this);
            Destroy(session.gameObject);
        }
    }
}
