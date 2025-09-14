namespace EV_Station.Api.Abstractions
{
    public interface IEndpointDefinition
    {
        public void RegisterEndpoints(WebApplication application);
    }
}
