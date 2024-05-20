using Android.Content;
using Android.OS;

namespace Asjc.Android.ServiceHelper
{
    public class ServiceConnection<T> : Java.Lang.Object, IServiceConnection where T : Service
    {
        public Binder<T>? Binder { get; private set; }
        public T? Service => Binder?.Service;

        public event Action<ComponentName?, Binder<T>?>? Connected;
        public event Action<ComponentName?>? Disconnected;

        public void OnServiceConnected(ComponentName? name, IBinder? service)
        {
            var binder = service as Binder<T>;
            Binder = binder;
            Connected?.Invoke(name, binder);
        }

        public void OnServiceDisconnected(ComponentName? name)
        {
            Binder = null;
            Disconnected?.Invoke(name);
        }
    }
}
