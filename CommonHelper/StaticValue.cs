using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonHelper
{
    public class StaticValue
    {
        public readonly static string ResourcePath = Appsettings.ReadNode("SysSetting:ResourcePath");

    }
}
