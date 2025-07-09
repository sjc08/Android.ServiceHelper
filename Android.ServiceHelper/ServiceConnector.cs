using Android.Content;
using Android.OS;

namespace Asjc.Android.ServiceHelper
{
    public class ServiceConnector : Java.Lang.Object, IServiceConnection
    {
        public ServiceBinder? Binder { get; private set; }

        /// <summary>
        /// Indicates whether the connection is valid.
        /// </summary>
        public bool IsConnected => Binder != null;

        /// <summary>
        /// Occurs when the service is connected.
        /// </summary>
        public event Action<ServiceConnector, Service>? Connected;

        /// <summary>
        /// Occurs when the service is disconnected.
        /// </summary>
        public event Action<ServiceConnector>? Disconnected;

        /// <summary>
        /// Executes an action when the service is connected.
        /// </summary>
        /// <remarks>
        /// If the service is already connected, <paramref name="action"/> is executed immediately.<br/>
        /// If the service is not yet connected, <paramref name="action"/> is executed once the service is connected.
        /// </remarks>
        /// <param name="action">The action to execute.</param>
        public void WhenConnected(Action<Service> action)
        {
            if (IsConnected)
            {
                action(Binder!.Service);
            }
            else
            {
                void Handler(ServiceConnector connector, Service service)
                {
                    Connected -= Handler;
                    action(service);
                }
                Connected += Handler;
            }
        }

        protected void OnConnected(ComponentName? name, ServiceBinder binder) => Connected?.Invoke(this, binder.Service);

        protected void OnDisconnected(ComponentName? name) => Disconnected?.Invoke(this);

        void IServiceConnection.OnServiceConnected(ComponentName? name, IBinder? service)
        {
            Binder = service as ServiceBinder;
            if (IsConnected)
                OnConnected(name, Binder!);
        }

        void IServiceConnection.OnServiceDisconnected(ComponentName? name)
        {
            Binder = null;
            OnDisconnected(name);
        }
    }

    /// <typeparam name="T">The type of the service.</typeparam>
    public class ServiceConnector<T> : Java.Lang.Object, IServiceConnection where T : Service
    {
        public ServiceBinder<T>? Binder { get; private set; }

        /// <summary>
        /// Indicates whether the connection is valid.
        /// </summary>
        public bool IsConnected => Binder != null;

        /// <summary>
        /// Occurs when the service is connected.
        /// </summary>
        public event Action<ServiceConnector<T>, T>? Connected;

        /// <summary>
        /// Occurs when the service is disconnected.
        /// </summary>
        public event Action<ServiceConnector<T>>? Disconnected;

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
            if (IsConnected)
            {
                action(Binder!.Service);
            }
            else
            {
                void Handler(ServiceConnector<T> connector, T service)
                {
                    Connected -= Handler;
                    action(service);
                }
                Connected += Handler;
            }
        }

        protected void OnConnected(ComponentName? name, ServiceBinder<T> binder) => Connected?.Invoke(this, binder.Service);

        protected void OnDisconnected(ComponentName? name) => Disconnected?.Invoke(this);

        void IServiceConnection.OnServiceConnected(ComponentName? name, IBinder? service)
        {
            Binder = service as ServiceBinder<T>;
            if (IsConnected)
                OnConnected(name, Binder!);
        }

        void IServiceConnection.OnServiceDisconnected(ComponentName? name)
        {
            Binder = null;
            OnDisconnected(name);
        }
    }
}
