using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace TransportsNantais.Enums
{
    public enum LineType
    {
        [Description("Tramway")]
        Tramway = 1,

        [Description("BusWay")]
        BusWay = 2,

        [Description("Bus")]
        Bus = 3,

        [Description("Navibus")]
        Navibus = 4
    }
}
