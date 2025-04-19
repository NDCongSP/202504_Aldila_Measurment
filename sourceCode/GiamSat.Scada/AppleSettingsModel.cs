using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GiamSat.Scada
{
    public class AppleSettingsModel
    {
        public List<EnumSensor> Senssors { get; set; }=new List<EnumSensor>();
        public List<AppleCheckValue> Result { get; set; }=new List<AppleCheckValue>();
    }

    public class AppleCheckValue
    {
        /// <summary>
        /// Ngưỡng dưới của vùng.
        /// </summary>
        public double LowLimit { get; set; } = 0;
        /// <summary>
        /// Ngưỡng trên của vùng.
        /// </summary>
        public double HightLimit { get; set; } = 1;
        /// <summary>
        /// tên của vùng.
        /// </summary>
        public EnumApple_Ok_NG Result { get; set; }
    }
}
