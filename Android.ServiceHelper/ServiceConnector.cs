using Android.Content;
using Android.OS;

namespace Asjc.Android.ServiceHelper
{
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
        public event Action<ComponentName?, ServiceBinder<T>?>? ServiceConnected;

        /// <summary>
        /// Occurs when the service is disconnected.
        /// </summary>
        public event Action<ComponentName?>? ServiceDisconnected;

        /// <summary>
        /// Executes an action when the service is connected.
        /// </summary>
        /// <remarks>
        /// If the service is already connected, the action is executed immediately.
        /// If the service is not yet connected, the action is executed once the service is connected.
        /// Note: Whether the service is connected is determined by the <see cref="Connected"/> property.
        /// </remarks>
        /// <param name="action">The action to execute.</param>
        public void WhenConnected(Action<T> action)
        {
            // Since Connected is true, Binder isn't null, which means Service isn't null.
            // Hence, action(Service) won't receive a null value for Service.
            if (Connected)
            {
                action(Service);
            }
            else
            {
                void Handler(ComponentName? name, ServiceBinder<T>? binder)
                {
                    if (Connected)
                    {
                        ServiceConnected -= Handler;
                        action(Service);
                    }
                }
                ServiceConnected += Handler;
            }
        }

        void IServiceConnection.OnServiceConnected(ComponentName? name, IBinder? service)
        {
            Binder = (ServiceBinder<T>?)service;
            ServiceConnected?.Invoke(name, Binder);
        }

        void IServiceConnection.OnServiceDisconnected(ComponentName? name)
        {
            Binder = null;
            ServiceDisconnected?.Invoke(name);
        }
    }
}
