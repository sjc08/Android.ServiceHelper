namespace Asjc.Android.ServiceHelper
{
    public class Binder<T>(T service) where T : Service
    {
        public T Service { get; } = service;
    }
}
