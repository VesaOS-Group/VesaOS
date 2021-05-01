using System;
using System.Collections.Generic;
using System.Text;
using IL2CPU.API.Attribs;

namespace VesaOS.Plugs
{
    [Plug(Target = typeof(global::System.Environment))]
    class EnviromentImpl
    {
        public static string GetEnvironmentVariable(string variable)
        {
            return "";
        }
    }
}
