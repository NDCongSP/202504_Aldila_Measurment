using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GiamSat.Scada
{
    public enum EnumSensor
    {
        SENSOR_1 = 1,
        SENSOR_2 = 2,
        SENSOR_3 = 3,
    }

    public enum EnumArrowZoneName
    {
        V1 = 1,
        V3 = 2,
        V6 = 3,
        V9 = 4,
        VSS = 5
    }

    public enum EnumArrow_OK_NG
    {
        Sensor3MinusSensor1 = 1, //
        Senor1MinusSensor3 = 2
    }

    public enum EnumApple_Ok_NG
    {
        OK = 1,
        NG = 2
    }
}
