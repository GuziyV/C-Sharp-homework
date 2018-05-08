using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Homework
{
    static class Settings
    {
        static Settings()
        {
            _parkingSpace = 25;
            _fine = 1.5m;
            _parking = Parking.Instance;
            _takeMoney = new ThreadStart(Timeout);
        }

        static private ThreadStart _takeMoney;

        static private Parking _parking;

        static public int NumberOfSeconds {get;set;}

        static Dictionary<CarType, decimal> prices = new Dictionary<CarType, decimal>()
        {
            {CarType.Motorcycle, 5},
            { CarType.Passenger, 10},
            { CarType.Truck, 15},
            { CarType.Bus, 20}
        };

        static private readonly uint _parkingSpace;

        static private readonly decimal _fine;

        private static void Timeout()
        {
            foreach(Car car in _parking.Cars)
            {
                decimal price = prices[car.Type];
                if ((car.Balance - price) < 0)
                {
                    price *= _fine;
                }
                car.GiveMoney(price);
                _parking.AddMoney(price);
            }
            Thread.Sleep(NumberOfSeconds * 1000);
        }
    }
}
