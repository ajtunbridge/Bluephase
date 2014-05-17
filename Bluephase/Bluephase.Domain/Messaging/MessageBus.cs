#region Using directives

using System;
using System.Collections.Generic;

#endregion

namespace Bluephase.Domain.Messaging
{
    /// <summary>
    ///     A Message Bus is a central messaging system for
    ///     an application.
    /// </summary>
    public sealed class MessageBus : IMessageBus
    {
        private readonly object _lock = new object();

        private readonly Dictionary<Type, List<ActionReference>> _subscribers =
            new Dictionary<Type, List<ActionReference>>();

        #region IMessageBus Members

        /// <summary>
        ///     Subscribes an action to a particular message type.  When that message type
        ///     is published, this action will be called.
        /// </summary>
        /// <typeparam name="TMessage">The type of message to listen for.</typeparam>
        /// <param name="handler">
        ///     The action which will be called when a message of type <typeparamref name="TMessage" />
        ///     is published.
        /// </param>
        public void Subscribe<TMessage>(Action<TMessage> handler)
        {
            lock (_lock) {
                if (_subscribers.ContainsKey(typeof (TMessage))) {
                    List<ActionReference> handlers = _subscribers[typeof (TMessage)];
                    handlers.Add(new ActionReference(handler));
                }
                else {
                    var handlers = new List<ActionReference>();
                    handlers.Add(new ActionReference(handler));
                    _subscribers[typeof (TMessage)] = handlers;
                }
            }
        }

        /// <summary>
        ///     Unsubscribes an action from a particular message type.
        /// </summary>
        /// <typeparam name="TMessage">The type of message to stop listening for.</typeparam>
        /// <param name="handler">
        ///     The action which is to be unsubscribed from messages of type <typeparamref name="TMessage" />.
        /// </param>
        public void Unsubscribe<TMessage>(Action<TMessage> handler)
        {
            lock (_lock) {
                if (_subscribers.ContainsKey(typeof (TMessage))) {
                    List<ActionReference> handlers = _subscribers[typeof (TMessage)];

                    ActionReference targetReference = null;
                    foreach (ActionReference reference in handlers) {
                        var action = (Action<TMessage>) reference.Target;
                        if ((action.Target == handler.Target) && action.Method.Equals(handler.Method)) {
                            targetReference = reference;
                            break;
                        }
                    }
                    handlers.Remove(targetReference);

                    if (handlers.Count == 0) {
                        _subscribers.Remove(typeof (TMessage));
                    }
                }
            }
        }

        /// <summary>
        ///     Publishes a message to any subscribers of a particular message type.
        /// </summary>
        /// <typeparam name="TMessage">The type of message to publish.</typeparam>
        /// <param name="message">The message to be published</param>
        public void Publish<TMessage>(TMessage message)
        {
            List<Action<TMessage>> subscribers = RefreshAndGetSubscribers<TMessage>();
            foreach (Action<TMessage> subscriber in subscribers) {
                subscriber.Invoke(message);
            }
        }

        /// <summary>
        ///     Publishes a message to any subscribers of a particular message type using
        ///     default instance of the message type
        /// </summary>
        /// <typeparam name="TMessage">The type of message to publish.</typeparam>
        public void Publish<TMessage>()
        {
            var message = Activator.CreateInstance<TMessage>();

            List<Action<TMessage>> subscribers = RefreshAndGetSubscribers<TMessage>();
            foreach (Action<TMessage> subscriber in subscribers) {
                subscriber.Invoke(message);
            }
        }

        #endregion

        private List<Action<TMessage>> RefreshAndGetSubscribers<TMessage>()
        {
            var toCall = new List<Action<TMessage>>();
            var toRemove = new List<ActionReference>();

            lock (_lock) {
                if (_subscribers.ContainsKey(typeof (TMessage))) {
                    List<ActionReference> handlers = _subscribers[typeof (TMessage)];
                    foreach (ActionReference handler in handlers) {
                        if (handler.IsAlive) {
                            toCall.Add((Action<TMessage>) handler.Target);
                        }
                        else {
                            toRemove.Add(handler);
                        }
                    }

                    foreach (ActionReference remove in toRemove) {
                        handlers.Remove(remove);
                    }

                    if (handlers.Count == 0) {
                        _subscribers.Remove(typeof (TMessage));
                    }
                }
            }

            return toCall;
        }
    }
}