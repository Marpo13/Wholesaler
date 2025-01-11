using Wholesaler.Backend.Domain.Exceptions;
using Wholesaler.Backend.Domain.Interfaces;

namespace Wholesaler.Backend.Api;

public class ErrorHandlingMiddleware : IMiddleware
{
    private readonly ITransaction _transaction;

    public ErrorHandlingMiddleware(ITransaction transaction)
    {
        _transaction = transaction;
    }

    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        try
        {
            await next.Invoke(context);
        }
        catch (Exception ex)
        {
            if (_transaction.IsStarted)
                _transaction.Rollback();

            switch (ex)
            {
                case UnpermittedActionPerformedException:
                    context.Response.StatusCode = 403;
                    await context.Response.WriteAsync(ex.Message);
                    break;

                case InvalidDataProvidedException:
                    context.Response.StatusCode = 400;
                    await context.Response.WriteAsync(ex.Message);
                    break;

                case EntityNotFoundException:
                    context.Response.StatusCode = 404;
                    await context.Response.WriteAsync(ex.Message);
                    break;

                case InvalidProcedureException:
                    context.Response.StatusCode = 500;
                    await context.Response.WriteAsync(ex.Message);
                    break;

                default:
                    context.Response.StatusCode = 500;
                    await context.Response.WriteAsync(ex.Message);
                    break;
            }
        }
     }
}
