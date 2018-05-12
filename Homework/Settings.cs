using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Parking
{
    static class Settings
    {
        static Settings()
        {
            //Config
            ParkingSpace = 6;
            Fine = 1.5m;
            NumberOfSeconds = 5;
            //Endconfig
        }

        static public readonly int NumberOfSeconds;

        static public readonly decimal Fine;

        static public readonly Dictionary<CarType, decimal> prices = new Dictionary<CarType, decimal>()
        {
            {CarType.Motorcycle, 5},
            { CarType.Passenger, 10},
            { CarType.Truck, 15},
            { CarType.Bus, 20}
        };

        static public readonly uint ParkingSpace;
    }
}
