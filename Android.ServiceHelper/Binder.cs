using Android.OS;

namespace Asjc.Android.ServiceHelper
{
    public class Binder<T>(T service) : Binder where T : Service
    {
        public T Service { get; } = service;
    }
}
