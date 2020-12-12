using System;
using System.Collections.Generic;

namespace CODE_GameLib.Utilities
{
    public class Provider<T> : IObservable<T>
    {
        private readonly List<IObserver<T>> observers = new List<IObserver<T>>();

        public IDisposable Subscribe(IObserver<T> observer)
        {
            if (!observers.Contains(observer)) observers.Add(observer);
            return new UnSubscriber<T>(observers, observer);
        }

        protected void Notify(T value)
        {
            foreach (var observer in observers)
                observer.OnNext(value);
        }
    }
}