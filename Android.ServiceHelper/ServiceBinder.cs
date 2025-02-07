using Android.OS;

namespace Asjc.Android.ServiceHelper
{
    public class ServiceBinder(Service service) : Binder
    {
        public Service Service { get; } = service;
    }

    /// <typeparam name="T">The type of the service.</typeparam>
    public class ServiceBinder<T>(T service) : Binder where T : Service
    {
        public T Service { get; } = service;
    }
}
