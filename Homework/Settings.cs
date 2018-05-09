using System;
using System.Collections.Generic;
using System.IO;
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
            //Config
            ParkingSpace = 25;
            _fine = 1.5m;
            _numberOfSeconds = 5;
            //Endconfig
            Parking = Parking.Instance;
            int minute = 60;
            _takeMoney = new Timer(new TimerCallback(writeTransactionToFile), null, 0, minute * 1000);
            _writeToFile = new Timer(new TimerCallback(Timeout), null, 0, _numberOfSeconds * 1000);

        }

        static private Timer _takeMoney;

        static private Timer _writeToFile;

        static public Parking Parking;

        static private int _numberOfSeconds;

        static Dictionary<CarType, decimal> prices = new Dictionary<CarType, decimal>()
        {
            {CarType.Motorcycle, 5},
            { CarType.Passenger, 10},
            { CarType.Truck, 15},
            { CarType.Bus, 20}
        };

        static public readonly uint ParkingSpace;

        static private readonly decimal _fine;

        private static void writeTransactionToFile(object obj)
        {
            {
                var lastMinuteTransactins = Parking.Transactions.
                    Where<Transaction>(t => DateTime.Now - t.TransactionTime < new TimeSpan(0, 1, 0));
                using (StreamWriter sw = new StreamWriter("Transactions.log", true, System.Text.Encoding.Default))
                {
                    sw.WriteLine("{0} transactions:", DateTime.Now);
                    foreach (var transaction in lastMinuteTransactins)
                    {
                        sw.WriteLine(transaction);
                    }
                }
            }
        }

        private static void Timeout(object obj)
        {
            foreach (Car car in Parking.Cars)
            {
                decimal price = prices[car.Type];
                if ((car.Balance - price) < 0)
                {
                    price *= _fine;
                }
                car.GiveMoney(price);
                Parking.AddMoney(price);
                Settings.Parking.addTransaction(new Transaction(car.Id, price));
            }
            Thread.Sleep(_numberOfSeconds * 1000);
            

        }
    }
}
