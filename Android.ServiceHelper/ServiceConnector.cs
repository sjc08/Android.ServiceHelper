using Android.Content;
using Android.OS;

namespace Asjc.Android.ServiceHelper
{
    public class ServiceConnector<T> : Java.Lang.Object, IServiceConnection where T : Service
    {
        public ServiceBinder<T>? Binder { get; private set; }

        public bool Connected => Binder != null;

        public T? Service => Binder?.Service;

        public event Action<ComponentName?, ServiceBinder<T>?>? ServiceConnected;
        public event Action<ComponentName?>? ServiceDisconnected;

        public void OnServiceConnected(ComponentName? name, IBinder? service)
        {
            Binder = (ServiceBinder<T>?)service;
            ServiceConnected?.Invoke(name, Binder);
        }

        public void OnServiceDisconnected(ComponentName? name)
        {
            Binder = null;
            ServiceDisconnected?.Invoke(name);
        }
    }
}
