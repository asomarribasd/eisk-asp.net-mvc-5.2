using System;
using System.Web.Mvc;
using Eisk.Helpers;

public class CustomExceptionFilter : HandleErrorAttribute
{
    public override void OnException(ExceptionContext context)
    {
        Exception ex = context.Exception;
        Logger.LogError(ex);
        base.OnException(context);
    }
}