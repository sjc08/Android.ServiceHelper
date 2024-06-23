using Android.OS;

namespace Asjc.Android.ServiceHelper
{
    public class ServiceBinder<T>(T service) : Binder where T : Service
    {
        public T Service { get; } = service;
    }
}
