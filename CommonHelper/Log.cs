using log4net;
using log4net.Config;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
[assembly: XmlConfigurator(ConfigFile = "log4net.config", Watch = true)]

namespace CommonHelper
{
    public class Log<T> where T : class
    {
        public readonly ILog _log4 = LogManager.GetLogger(typeof(T));
    }
}
