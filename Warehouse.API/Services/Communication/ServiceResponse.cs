namespace Warehouse.API.Services.Communication;

public class ServiceResponse<T>
{
    public ServiceResponse(string errorMessage)
    {
        ErrorMessage = errorMessage;
        Success = false;
    }

    public ServiceResponse(T resource)
    {
        Resource = resource;
        Success = true;
    }

    public bool Success { get; protected set; }
    public string ErrorMessage { get; protected set; } = string.Empty;
    public T? Resource { get; }
}