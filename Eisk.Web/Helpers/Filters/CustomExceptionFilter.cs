using System.Web.Mvc;
using Eisk.Helpers;

public class CustomExceptionFilter : HandleErrorAttribute
{
    public override void OnException(ExceptionContext context)
    {
        var ex = context.Exception;
        Logger.LogError(ex);
        base.OnException(context);
    }
}