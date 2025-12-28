using Godot;
using MessagePipe;
using Microsoft.Extensions.DependencyInjection;
using System;

public static class MessagePipeExtensions
{
    // Extension per il Publisher
    public static void PublishEvent<T>(this Node node, T message)
    {
        var publisher = GameManager.Services.GetRequiredService<IPublisher<T>>();
        publisher.Publish(message);
    }

    // Extension per il Subscriber
    // Ritorna un IDisposable
    public static IDisposable SubscribeEvent<T>(this Node node, Action<T> action)
    {
        var subscriber = GameManager.Services.GetRequiredService<ISubscriber<T>>();
        return subscriber.Subscribe(action);
    }

	// Nelle estensioni aggiungi questo:
	public static void AddTo(this IDisposable disposable, DisposableBagBuilder bag)
    {
        if (disposable == null) return;
        
        // Il metodo corretto del builder è .Add()
        bag.Add(disposable);
    }
}