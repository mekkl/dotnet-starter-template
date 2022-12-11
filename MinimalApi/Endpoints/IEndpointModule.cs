namespace MinimalApi.Endpoints;

public interface IEndpointModule
{
    public void AddRoutes(IEndpointRouteBuilder app);
}