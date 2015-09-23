using System.Diagnostics.CodeAnalysis;
using Eisk.Helpers;

namespace Eisk
{
    public class DependencyInjectorInitializer
    {
        [SuppressMessage("Microsoft.Reliability",
            "CA2000:Dispose objects before losing scope",
            Justification = "This should survive the lifetime of the application.")]
        public static void Init()
        {
            DependencyHelper.Initialize();
        }
    }
}