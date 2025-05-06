using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GiamSat.Scada
{
    public class Configs
    {
        public string ConfigName { get; set; } = string.Empty;
        public ConfigModel Config { get; set; } = new ConfigModel();
    }
}
