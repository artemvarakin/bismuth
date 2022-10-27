using Grpc.Core;
using Grpc.Core.Interceptors;

namespace UserAPI.Infrastructure;

public class ExceptionInterceptor : Interceptor
{
    private readonly ILogger<ExceptionInterceptor> _logger;

    public ExceptionInterceptor(ILogger<ExceptionInterceptor> logger)
    {
        _logger = logger;
    }

    public async override Task<TResponse> UnaryServerHandler<TRequest, TResponse>(
        TRequest request,
        ServerCallContext context,
        UnaryServerMethod<TRequest, TResponse> continuation)
    {
        try
        {
            return await continuation(request, context);
        }
        catch (Exception e) when (e is not RpcException ex
            || ex.StatusCode != StatusCode.InvalidArgument)
        {
            _logger.LogError(
                e,
                "An error occurred when calling {method}: {message}",
                context.Method,
                e.Message);

            throw;
        }
    }
}