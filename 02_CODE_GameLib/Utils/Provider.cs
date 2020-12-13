using System;
using System.Collections.Generic;

namespace CODE_GameLib.Utilities
{
    public class Provider<T> : IObservable<T>
    {
        private readonly List<IObserver<T>> Observers = new List<IObserver<T>>();

        public IDisposable Subscribe(IObserver<T> observer)
        {
            if (!Observers.Contains(observer)) Observers.Add(observer);
            return new UnSubscriber<T>(Observers, observer);
        }

        protected void Notify(T value)
        {
            foreach (var observer in Observers)
                observer.OnNext(value);
        }
    }
}