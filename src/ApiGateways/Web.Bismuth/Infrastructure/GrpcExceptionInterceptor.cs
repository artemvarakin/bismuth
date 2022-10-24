using Grpc.Core;
using Grpc.Core.Interceptors;

namespace Web.Bismuth.Infrastructure;

internal class GrpcExceptionInterceptor : Interceptor
{
    private readonly ILogger<GrpcExceptionInterceptor> _logger;

    public GrpcExceptionInterceptor(ILogger<GrpcExceptionInterceptor> logger)
    {
        _logger = logger;
    }

    public override AsyncUnaryCall<TResponse> AsyncUnaryCall<TRequest, TResponse>(
        TRequest request,
        ClientInterceptorContext<TRequest, TResponse> context,
        AsyncUnaryCallContinuation<TRequest, TResponse> continuation)
    {
        var call = continuation(request, context);
        return new AsyncUnaryCall<TResponse>(
            HadnleResponseAsync(call.ResponseAsync)!,
            call.ResponseHeadersAsync,
            call.GetStatus,
            call.GetTrailers,
            call.Dispose);
    }

    private async Task<TResponse?> HadnleResponseAsync<TResponse>(Task<TResponse> task)
    {
        try
        {
            return await task;
        }
        catch (RpcException e)
        {
            if (e.StatusCode != StatusCode.NotFound)
            {
                throw new InvalidOperationException("Error processing request.", e);
            }

            return default;
        }
    }
}