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
            ParkingSpace = 6;
            _fine = 1.5m;
            _numberOfSeconds = 5;
            //Endconfig
            Parking = Parking.Instance;
            int minute = 60;
            _writeToFile = new Timer(new TimerCallback(writeTransactionToFile), null, 1000, minute * 1000);
            _takeMoney = new Timer(new TimerCallback(Timeout), null, 0, _numberOfSeconds * 1000);
        }

        private static StreamWriter _log = new StreamWriter("Transactions.log", true, System.Text.Encoding.Default);

        static private Timer _takeMoney;

        static private Timer _writeToFile;

        static private int _numberOfSeconds;

        static private readonly decimal _fine;

        static public Parking Parking;

        static Dictionary<CarType, decimal> prices = new Dictionary<CarType, decimal>()
        {
            {CarType.Motorcycle, 5},
            { CarType.Passenger, 10},
            { CarType.Truck, 15},
            { CarType.Bus, 20}
        };

        static public uint ParkingSpace { get; private set; }

        private static void writeTransactionToFile(object obj)
        {
            {
                var lastMinuteTransactins = Parking.Transactions.
                    Where<Transaction>(t => DateTime.Now - t.TransactionTime < new TimeSpan(0, 1, 0));              
                _log.WriteLine("Date and time: {0}", DateTime.Now);
                decimal sum = 0;
                foreach (var transaction in lastMinuteTransactins)
                {
                    sum += transaction.Withdraw;
                }
                _log.WriteLine("Sum: {0:0.00}", sum);
                
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
                Parking.AddTransaction(new Transaction(car.Id, price));
            }
        }

        public static void StopWorking()
        {
            _log.Dispose();
            _takeMoney.Dispose();
            _writeToFile.Dispose();
        }
    }
}
