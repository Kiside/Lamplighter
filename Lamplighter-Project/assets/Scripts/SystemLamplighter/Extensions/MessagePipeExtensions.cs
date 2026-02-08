using Godot;
using MessagePipe;
using Microsoft.Extensions.DependencyInjection;
using System;
using SystemLamplighter.Bootstrap;

namespace SystemLamplighter.Extensions
{
    /// <summary>
    /// Estensioni per il MessagePipe
    /// </summary>
    public static class MessagePipeExtensions
    {
        // Extension per il Publisher
        public static void PublishEvent<T>(this Node node, T message)
        {
            var publisher = GameBootstrap.Services.GetRequiredService<IPublisher<T>>();
            publisher.Publish(message);
        }

        public static IPublisher<T> GetPublisher<T>(this Node node)
        {
            return GameBootstrap.Services.GetRequiredService<IPublisher<T>>();
        }

        public static ISubscriber<T> GetSubscriber<T>(this Node node)
        {
            return GameBootstrap.Services.GetRequiredService<ISubscriber<T>>();
        }

        // Extension per il Subscriber
        // Ritorna un IDisposable
        public static IDisposable SubscribeEvent<T>(this Node node, Action<T> action)
        {
            var subscriber = GameBootstrap.Services.GetRequiredService<ISubscriber<T>>();
            return subscriber.Subscribe(action);
        }

        // Extension per il Publisher for Resource
        public static void PublishEventResource<T>(this Resource node, T message)
        {
            var publisher = GameBootstrap.Services.GetRequiredService<IPublisher<T>>();
            publisher.Publish(message);
        }

        // Extension per il Subscriber for Resource
        // Ritorna un IDisposable
        public static IDisposable SubscribeEventResource<T>(this Resource node, Action<T> action)
        {
            var subscriber = GameBootstrap.Services.GetRequiredService<ISubscriber<T>>();
            return subscriber.Subscribe(action);
        }


        public static IPublisher<T> GetPublisher<T>(this Resource node)
        {
            return GameBootstrap.Services.GetRequiredService<IPublisher<T>>();
        }

        public static ISubscriber<T> GetSubscriber<T>(this Resource node)
        {
            return GameBootstrap.Services.GetRequiredService<ISubscriber<T>>();
        }
    }
}