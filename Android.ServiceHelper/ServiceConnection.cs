using Android.Content;
using Android.OS;

namespace Asjc.Android.ServiceHelper
{
    public class ServiceConnection<T> : Java.Lang.Object, IServiceConnection where T : Service
    {
        private readonly ManualResetEventSlim mres = new();

        public Binder<T>? Binder { get; private set; }

        public bool Connected => Binder != null;

        public T? Service => Binder?.Service;

        public event Action<ComponentName?, Binder<T>?>? ServiceConnected;
        public event Action<ComponentName?>? ServiceDisconnected;

        public void WaitUntilConnected() => mres.Wait();

        public void OnServiceConnected(ComponentName? name, IBinder? service)
        {
            Binder = (Binder<T>?)service;
            mres.Set();
            ServiceConnected?.Invoke(name, Binder);
        }

        public void OnServiceDisconnected(ComponentName? name)
        {
            Binder = null;
            mres.Reset();
            ServiceDisconnected?.Invoke(name);
        }
    }
}
