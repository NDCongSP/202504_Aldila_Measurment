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
        /// Giá trị ngưỡng để kích hoạt bắt đầu đo.
        /// Chỉ cho phép đo khi các giá trị của sensor nhỏ hơn giá trị này.
        /// </summary>
        public double ValueActive { get; set; } = 800;
        public bool ActiveCheckHeadStraight { get; set; } = true;

        /// <summary>
        /// Settings cho phần đo ARROW.
        /// </summary>
        public ArrowSettingsModel ArrowSettings { get; set; } = new ArrowSettingsModel();
        /// <summary>
        /// Settings cho phần apple.
        /// </summary>
        public AppleSettingsModel AppleSettings { get; set; } = new AppleSettingsModel();
    }
}
