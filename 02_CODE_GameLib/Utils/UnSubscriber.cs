using System;
using System.Collections.Generic;

namespace CODE_GameLib.Utilities
{
    public class UnSubscriber<T> : IDisposable
    {
        private readonly List<IObserver<T>> observers;
        private readonly IObserver<T> observer;

        public UnSubscriber(List<IObserver<T>> observers, IObserver<T> observer)
        {
            this.observers = observers;
            this.observer = observer;
        }

        public void Dispose()
        {
            observers.Remove(observer);
        }
    }
}