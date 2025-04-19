using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GiamSat.Scada
{
    public class AppleSettingsModel
    {
        /// <summary>
        /// chọn senor để so sánh phân zone. Chọn được nhiều seenssor cùng lúc.
        /// </summary>
        public List<EnumSensor> Sensors { get; set; }=new List<EnumSensor>();
        /// <summary>
        /// Data lớn nhất hay nhỏ nhất. Chỉ được chọn 1 trong 2.
        /// Dùng cho trường hợp chọn nhiều sensor cùng lúc để so sánh.
        /// True là lấy data lớn nhất để so sánh, False lấy data nhỏ nhất để so sánh.
        /// </summary>
        public bool DataMax { get; set; } = true;
        public List<AppleZone> Zones { get; set; }=new List<AppleZone>();
    }

    public class AppleZone
    {
        /// <summary>
        /// Ngưỡng dưới của vùng.
        /// </summary>
        public double FromValue { get; set; } = 0;
        /// <summary>
        /// Ngưỡng trên của vùng.
        /// </summary>
        public double ToValue { get; set; } = 1;
        /// <summary>
        /// Tên của vùng.
        /// </summary>
        public EnumApple_Ok_NG ZoneName { get; set; }
    }
}
