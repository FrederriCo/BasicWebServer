namespace BasicWebServer.Server.Common
{
    public interface IServiceCollection
    {
        IServiceCollection Add<TService, TImplementation>()
            where TService : class;
    }
}
