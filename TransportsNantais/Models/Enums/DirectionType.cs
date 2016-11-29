using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace TransportsNantais.Enums
{
    public enum DirectionType
    {
        [Description("Terminus 1")]
        OneDirection = 1,

        [Description("Terminus 2")]
        OppositeDirection = 2
    }
}
