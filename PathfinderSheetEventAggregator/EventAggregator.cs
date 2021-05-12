using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace PathfinderSheetModels
{
    public interface IEventAggregator
    {
        void Subscribe(object subscriber);
        void Unsubscribe(object subscriber);
        void Publish<T>(T message);
    }

    public interface IHandle<TMessage>
    {
        void Handle(TMessage message);
    }

    public class EventAggregator : IEventAggregator
    {
        private readonly List<Handler> _handlers = new List<Handler>();

        public void Subscribe(object subscriber)
        {
            lock (_handlers)
            {
                if (_handlers.Any(x => x.Matches(subscriber)))
                    return;

                _handlers.Add(new Handler(subscriber));
            }
        }

        public void Unsubscribe(object subscriber)
        {
            if (subscriber == null)
                throw new ArgumentNullException(nameof(subscriber));

            lock (_handlers)
            {
                var found = _handlers.FirstOrDefault(x => x.Matches(subscriber));

                if (found != null)
                    _handlers.Remove(found);
            }
        }

        public void Publish<T>(T message)
        {
            if (message == null)
                throw new ArgumentNullException(nameof(message));

            foreach (Handler h in _handlers)
                h.Handle(message.GetType(), message);

            var deadHandlers = _handlers.Where(h => h.IsDead);

            if (deadHandlers.Any())
                foreach (Handler deadHandler in deadHandlers)
                    _handlers.Remove(deadHandler);
        }

        private class Handler
        {
            private readonly WeakReference _reference;

            //a collection of interface types in their associated methods
            private readonly Dictionary<Type, MethodInfo> _supportedHandlers = new Dictionary<Type, MethodInfo>();

            public bool IsDead => _reference.Target == null;

            public Handler(object handler)
            {
                _reference = new WeakReference(handler);

                var interfaces = handler.GetType().GetTypeInfo().ImplementedInterfaces
                    .Where(x => x.GetTypeInfo().IsGenericType && x.GetGenericTypeDefinition() == typeof(IHandle<>));

                foreach (var i in interfaces)
                {
                    var type = i.GetTypeInfo().GenericTypeArguments[0];
                    var method = i.GetRuntimeMethod("Handle", new Type[] { type });

                    if (method != null)
                        _supportedHandlers[type] = method;
                }
            }

            public bool Matches(object instance)
            {
                return _reference.Target == instance;
            }

            public void Handle(Type messageType, object message)
            {
                var target = _reference.Target;

                var toHandle = _supportedHandlers.Where(handler => handler.Key.GetTypeInfo().IsAssignableFrom(messageType.GetTypeInfo()));

                try
                {
                    foreach (KeyValuePair<Type, MethodInfo> h in toHandle)
                        h.Value.Invoke(target, new[] { message });
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    Console.WriteLine(ex.InnerException);
                }
            }
        }
    }
}
