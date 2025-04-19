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
        public string TagPath { get; set; } = "Local Station/Channel1/Device";
        public double Gain { get; set; } = 1;
        public double Offset { get; set; } = 0;
        public string Unit { get; set; } = "mil";

        /// <summary>
        /// Settings cho phần đo ARROW.
        /// </summary>
        public ArrowSettingsModel ArrowSettings { get; set; } = new ArrowSettingsModel();
        public AppleSettingsModel AppleSettings { get; set; } = new AppleSettingsModel();
    }
}
