using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GiamSat.Scada
{
    public class ArrowSettingsModel
    {
        /// <summary>
        /// Nếu chọn sensor nào thì sẽ lấy giá trị sensor đó để phân zone, nếu chọn cả 3 thì sẽ lấy giá trị Lớn nhất hoặc nhỏ nhất theo cài đặt phía dưới để phân zone.
        /// </summary>
        public List<EnumSensor> Sensors { get; set; } = new List<EnumSensor>();

        /// <summary>
        /// Data lớn nhất hay nhỏ nhất. 
        /// Dùng cho trường hợp chọn nhiều sensor cùng lúc để so sánh
        /// True là lấy data lớn nhất để so sán, False lấy data nhỏ nhất để so sánh.
        /// </summary>
        public bool DataMax { get; set; } = true;

        /// <summary>
        /// Chọn sensor so sánh phân zone theo điều kiện.
        /// </summary>
        public ArrowChooseSensorAddCompare ArrowChooseSensorAddCompareSettings { get; set; }=new ArrowChooseSensorAddCompare();

        /// <summary>
        /// Xác định phân vùng theo giá trị cảm biến.
        /// </summary>
        public List<ArrowZone> Zones { get; set; } = new List<ArrowZone>();
        /// <summary>
        /// Chỉ được chọn 1 trong 2.
        /// True: Sensor 3 - Sensor 1.
        /// False: Sensor 1 - Sensor 3.
        /// </summary>
        public bool S3_S1OrS1_S3 { get; set; } = true;
        /// <summary>
        /// Giá trị dùng để so sánh.
        /// nếu kết quả 2 sensor trừ nhau >= giá trị này là OK, nhỏ hơn là NG
        /// </summary>
        public double CompareValue { get; set; } = 0;
    }

    /// <summary>
    /// ARROW. Model cài đặt giới hạn cho các vùng.
    /// </summary>
    public class ArrowZone
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
        /// tên của vùng.
        /// </summary>
        public EnumArrowZoneName ZonName { get; set; }
    }

    /// <summary>
    /// Arrow. model cài đặt để add thêm sensor vào để so sánh.
    /// chỉ khả dụng khi bước chọn sensor chọn <=2 sensor.
    /// </summary>
    public class ArrowChooseSensorAddCompare
    {
        /// <summary>
        /// sensor chọn để add thêm vào điều kiện so sánh.
        /// chỉ chọn được những con sensor mà chưa được chọn ở ArrowChooseSensorCompare.Sensors.
        /// </summary>
        public List<EnumSensor> Sensors { get; set; }= new List<EnumSensor>();
        /// <summary>
        /// Nếu giá trị sensor được chọn lớn hơn giá trị này thì sẽ áp dụng các Sensor này cùng với ArrowChooseSensorCompare.Sensors để phân zone.
        /// Nếu nhỏ hơn hoặc bằng thì không quan tâm.
        /// </summary>
        public double ConditionsValue { get; set; } = 0;
    }
}
