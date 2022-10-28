using Calzolari.Grpc.Net.Client.Validation;
using FluentValidation;
using FluentValidation.Results;
using Grpc.Core;
using Grpc.Core.Interceptors;

namespace Web.Bismuth.Infrastructure;

internal class GrpcExceptionInterceptor : Interceptor
{
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
            switch (e.StatusCode)
            {
                // resource not found or access denied
                case StatusCode.NotFound:
                case StatusCode.PermissionDenied:
                    return default;

                // validation errors
                case StatusCode.InvalidArgument:
                    var failures = e.GetValidationErrors().Select(err =>
                        new ValidationFailure(err.PropertyName, err.ErrorMessage));
                    throw new ValidationException(failures);

                // internal errors
                default:
                    throw new InvalidOperationException("Error processing request.", e);
            }
        }
    }
}