using System;

namespace Eisk.Helpers
{
    public static class ExceptionDude
    {
        public static string FormatMessage(Exception ex, bool log=true)
        {
            if (log)
                Logger.LogError(ex);
            
            string message = "Error: There was a problem while processing your request: " + ex.Message;

            if (ex.InnerException != null)
            {
                Exception inner = ex.InnerException;
                if (inner is System.Data.Common.DbException)
                    message = "Our database is currently experiencing problems. " + inner.Message;
                else if (inner is NullReferenceException)
                    message = "There are one or more required fields that are missing.";
                else
                {
                    var exception = inner as ArgumentException;
                    if (exception != null)
                    {
                        string paramName = exception.ParamName;
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