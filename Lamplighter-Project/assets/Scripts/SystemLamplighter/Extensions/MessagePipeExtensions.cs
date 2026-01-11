using Godot;
using MessagePipe;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace SystemLamplighter.Extensions
{
    public static class MessagePipeExtensions
    {
        // Extension per il Publisher
        public static void PublishEvent<T>(this Node node, T message)
        {
            var publisher = MessagePipeManager.Services.GetRequiredService<IPublisher<T>>();
            publisher.Publish(message);
        }

        // Extension per il Subscriber
        // Ritorna un IDisposable
        public static IDisposable SubscribeEvent<T>(this Node node, Action<T> action)
        {
            var subscriber = MessagePipeManager.Services.GetRequiredService<ISubscriber<T>>();
            return subscriber.Subscribe(action);
        }

        // Extension per il Publisher for Resource
        public static void PublishEventResource<T>(this Resource node, T message)
        {
            var publisher = MessagePipeManager.Services.GetRequiredService<IPublisher<T>>();
            publisher.Publish(message);
        }

        // Extension per il Subscriber for Resource
        // Ritorna un IDisposable
        public static IDisposable SubscribeEventResource<T>(this Resource node, Action<T> action)
        {
            var subscriber = MessagePipeManager.Services.GetRequiredService<ISubscriber<T>>();
            return subscriber.Subscribe(action);
        }

        // Nelle estensioni aggiungi questo:
        // public static void AddTo(this IDisposable disposable, DisposableBagBuilder bag)
        // {
        //     if (disposable == null) return;
            
        //     // Il metodo corretto del builder è .Add()
        //     bag.Add(disposable);
        // }
    }
}