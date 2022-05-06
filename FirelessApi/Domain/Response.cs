namespace FirelessApi.Domain;

public class Response<T> where T : class
{
    public bool Success { get;  set; } // PROTECTED SET?
    public string Message { get; protected set; }
    public T Resource { get; set; }

    public Response(T resource)
    {
        Resource = resource;
        Success = true;
        Message = "Success";
    }

    public Response(string message)
    {
        Success = false;
        Message = message;
    }
}