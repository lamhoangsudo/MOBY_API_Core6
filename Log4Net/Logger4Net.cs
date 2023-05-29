using log4net.Config;
using log4net;
using System.Reflection;
using Log4Net.LogUtility;
namespace MOBY_API_Core6.Log4Net
{
    public class Logger4Net
    {
        public void Loggers(Exception ex)
        {
            try
            {
                var logRepository = LogManager.GetRepository(Assembly.GetEntryAssembly());
                var log = new Logger();
                XmlConfigurator.Configure(logRepository, new FileInfo("Log4Net\\log4netconfig.config"));
                log.Info("Starting the web application");
                log.Error(ex.Message, ex.InnerException);
            }
            catch
            {
                throw;
            }
        }
    }
}
