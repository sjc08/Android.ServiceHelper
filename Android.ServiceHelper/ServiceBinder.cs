using Android.OS;

namespace Asjc.Android.ServiceHelper
{
    /// <typeparam name="T">The type of the service.</typeparam>
    public class ServiceBinder<T>(T service) : Binder where T : Service
    {
        /// <summary>
        /// The bound service.
        /// </summary>
        public T Service { get; } = service;
    }
}
