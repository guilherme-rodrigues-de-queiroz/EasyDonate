namespace EasyDonate.Application.DTOs.Responses;

public record ResponseModel<T>(
        bool isSuccess,
        int StatusCode,
        string Message,
        T? Data = default
    )
{
    public static ResponseModel<T> Ok(T? data, string message)
        => new(true, 200, message, data);

    public static ResponseModel<T> Created(T? data, string message)
        => new(true, 201, message, data);

    public static ResponseModel<T?> NoContent()
        => new(true, 204, "");

    public static ResponseModel<T?> BadRequest(string message)
        => new(false, 400, message);

    public static ResponseModel<T?> Unauthorized(string message = "Acesso negado. Credenciais inválidas ou ausentes.")
        => new(false, 401, message);

    public static ResponseModel<T?> Forbidden(string message = "Você não tem permissão para acessar este recurso.")
        => new(false, 403, message);

    public static ResponseModel<T?> NotFound(string message)
        => new(false, 404, message);

    public static ResponseModel<T?> Conflict(string message)
        => new(false, 409, message);

    public static ResponseModel<T?> UnprocessableEntity(string message)
        => new(false, 422, message);

    public static ResponseModel<T?> Fail(string message)
        => new(false, 500, message);
}
