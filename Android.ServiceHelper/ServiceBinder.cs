using Android.OS;

namespace Asjc.Android.ServiceHelper
{
    public class ServiceBinder : Binder
    {
        public ServiceBinder() { }

        public ServiceBinder(Service service) => Service = service;

        public required Service Service { get; init; }
    }

    /// <typeparam name="T">The type of the service.</typeparam>
    public class ServiceBinder<T> : Binder where T : Service
    {
        public ServiceBinder() { }

        public ServiceBinder(T service) => Service = service;

        public required T Service { get; init; }
    }
}
