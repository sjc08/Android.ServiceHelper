using Android.Content;
using Android.OS;

namespace Asjc.Android.ServiceHelper
{
    /// <typeparam name="T">The type of the service.</typeparam>
    public class ServiceConnector<T> : Java.Lang.Object, IServiceConnection where T : Service
    {
        /// <summary>
        /// The binder associated with the connected service.
        /// </summary>
        public ServiceBinder<T>? Binder { get; private set; }

        /// <summary>
        /// Indicates whether the connection is valid.
        /// </summary>
        public bool Connected => Binder != null;

        /// <summary>
        /// The bound service.
        /// </summary>
        public T? Service => Binder?.Service;

        /// <summary>
        /// Occurs when the service is connected.
        /// </summary>
        public event Action<ComponentName?, ServiceBinder<T>>? ServiceConnected;

        /// <summary>
        /// Occurs when the service is disconnected.
        /// </summary>
        public event Action<ComponentName?>? ServiceDisconnected;

        /// <summary>
        /// Executes an action when the service is connected.
        /// </summary>
        /// <remarks>
        /// If the service is already connected, <paramref name="action"/> is executed immediately.<br/>
        /// If the service is not yet connected, <paramref name="action"/> is executed once the service is connected.
        /// </remarks>
        /// <param name="action">The action to execute.</param>
        public void WhenConnected(Action<T> action)
        {
            // Maybe Service can't be null.
            if (Connected)
            {
                action(Service!);
            }
            else
            {
                void Handler(ComponentName? name, ServiceBinder<T> binder)
                {
                    ServiceConnected -= Handler;
                    action(Service!);
                }
                ServiceConnected += Handler;
            }
        }

        void IServiceConnection.OnServiceConnected(ComponentName? name, IBinder? service)
        {
            Binder = service as ServiceBinder<T>;
            if (Connected)
                ServiceConnected?.Invoke(name, Binder!);
        }

        void IServiceConnection.OnServiceDisconnected(ComponentName? name)
        {
            if (Connected)
            {
                Binder = null;
                ServiceDisconnected?.Invoke(name);
            }
        }
    }
}
