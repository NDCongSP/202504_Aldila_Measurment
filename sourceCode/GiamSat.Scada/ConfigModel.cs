using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GiamSat.Scada
{
    public class ConfigModel
    {
        public int DecimalNum { get; set; } = 3;

        /// <summary>
        /// Settings cho phần đo ARROW.
        /// </summary>
        public ArrowSettingsModel ArrowSettings { get; set; }
    }
}
