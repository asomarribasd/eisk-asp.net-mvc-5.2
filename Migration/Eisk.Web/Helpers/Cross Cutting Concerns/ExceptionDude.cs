using System;
using System.Data.Common;

namespace Eisk.Helpers
{
    public static class ExceptionDude
    {
        public static string FormatMessage(Exception ex, bool log = true)
        {
            if (log)
                Logger.LogError(ex);

            var message = "Error: There was a problem while processing your request: " + ex.Message;

            if (ex.InnerException != null)
            {
                var inner = ex.InnerException;
                if (inner is DbException)
                    message = "Our database is currently experiencing problems. " + inner.Message;
                else if (inner is NullReferenceException)
                    message = "There are one or more required fields that are missing.";
                else
                {
                    var exception = inner as ArgumentException;
                    if (exception != null)
                    {
                        var paramName = exception.ParamName;
                        message = string.Concat("The ", paramName, " value is illegal.");
                    }
                    else if (inner is ApplicationException)
                        message = "Exception in application" + inner.Message;
                    else
                        message = inner.Message;
                }
            }

            return message;
        }
    }
}