using Android.OS;

namespace Asjc.Android.ServiceHelper
{
    public class ServiceBinder<T>(T service) : Binder where T : Service
    {
        /// <summary>
        /// The bound service.
        /// </summary>
        public T Service { get; } = service;
    }
}
