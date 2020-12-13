using System;
using System.Collections.Generic;

namespace CODE_GameLib.Utilities
{
    public class UnSubscriber<T> : IDisposable
    {
        private readonly List<IObserver<T>> Observers;
        private readonly IObserver<T> Observer;

        public UnSubscriber(List<IObserver<T>> observers, IObserver<T> observer)
        {
            this.Observers = observers;
            this.Observer = observer;
        }

        public void Dispose()
        {
            Observers.Remove(Observer);
        }
    }
}