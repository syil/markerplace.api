using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace MarketplaceApi.Utilities;

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
public class ExceptionToProblemFilterAttribute : ExceptionFilterAttribute, IAsyncActionFilter
{
    private readonly ILoggerFactory loggerFactory;
    private Controller? controller;

    public ExceptionToProblemFilterAttribute(ILoggerFactory loggerFactory)
	{
        this.loggerFactory = loggerFactory;
    }

    public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        controller = (Controller)context.Controller;

        await next();
    }

    public override Task OnExceptionAsync(ExceptionContext context)
    {
        var logger = loggerFactory.CreateLogger(context.ActionDescriptor.DisplayName ?? nameof(ExceptionFilterAttribute));
        logger.LogError(context.Exception, "An error occured");

        context.Result = controller?.Problem(detail: context.Exception.Message) ?? new ObjectResult(context.Exception.Message);

        return Task.CompletedTask;
    }
}
