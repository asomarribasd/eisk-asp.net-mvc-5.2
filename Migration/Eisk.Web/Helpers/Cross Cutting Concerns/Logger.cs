using System;
using System.IO;
using System.Web;

namespace Eisk.Helpers
{

    public static class Logger
    {
        public static void LogError(Exception ex)
        {
            var currentContext = HttpContext.Current;

            string logSummery, logDetails, filePath = "No file path found.", url = "No url found to be reported.";

            if (currentContext != null && !(ex is HttpException)) //ignore "file not found")
            {
                filePath = currentContext.Request.FilePath;
                url = currentContext.Request.Url.AbsoluteUri;
                logSummery = ex.Message;
                logDetails = ex.ToString();

                //-----------------------------------------------------

                string path = currentContext.Server.MapPath("~/App_Data/log.txt");
                FileStream fStream = new FileStream(path, FileMode.Append, FileAccess.Write);
                BufferedStream bfs = new BufferedStream(fStream);
                StreamWriter sWriter = new StreamWriter(bfs);

                //insert a separator line
                sWriter.WriteLine("=================================================================================================");

                //create log for header
                sWriter.WriteLine(logSummery);
                sWriter.WriteLine("Log time:" + DateTime.Now);
                sWriter.WriteLine("URL: " + url);
                sWriter.WriteLine("File Path: " + filePath);

                //create log for body
                sWriter.WriteLine(logDetails);

                //insert a separator line
                sWriter.WriteLine("=================================================================================================");

                sWriter.Close();
            }

        }
    }
}



