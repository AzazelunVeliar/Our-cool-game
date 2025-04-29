public static class ServiceLocator
{
    private static System.Collections.Generic.Dictionary<System.Type, object> services = 
        new System.Collections.Generic.Dictionary<System.Type, object>();

    public static void Register<T>(T service) where T : class
    {
        services[typeof(T)] = service;
    }

    public static T Get<T>() where T : class
    {
        if (services.TryGetValue(typeof(T), out object service))
        {
            return service as T;
        }
        return null;
    }
}