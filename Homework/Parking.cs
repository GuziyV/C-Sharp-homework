using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Homework
{
    class Parking : IDisposable
    {
        private static readonly Lazy<Parking> lazy = new Lazy<Parking>(() => new Parking());

        public static Parking Instance { get { return lazy.Value; } }

        static private Timer _takeMoney;

        static private Timer _writeToFile;

        private Parking()
        {
            Cars = new List<Car>();
            Transactions = new List<Transaction>();
            int minute = 60;
            _writeToFile = new Timer(new TimerCallback(writeTransactionToFile), null, 1000, minute * 1000);
            _takeMoney = new Timer(new TimerCallback(Timeout), null, 0, Settings.NumberOfSeconds * 1000);
        }

        private List<Car> Cars;
        private List<Transaction> Transactions;
        public decimal Balance { get; private set; }

        public void AddCar(Car car)
        {
            if (Settings.Parking.Cars.Count >= Settings.ParkingSpace)
            {
                throw new NotEnoughSpaceException("Not enough space in the parking lot, please wait");
            }
            else
            {
                Cars.Add(car);
            }
        }

        public void AddTransaction(Transaction transaction)
        {
            Transactions.Add(transaction);
        }

        public void AddMoney(decimal amount)
        {
            Balance += amount;
        }

        public bool AddCarMoney(uint carId, decimal amount)
        {
            Car car = Cars.First<Car>(c => c.Id == carId);
            if (car == null)
            {
                return false;
            }
            car.AddMoney(amount);
            return true;
        }
        public void DeleteCar(uint carId)
        {
            Car car = Cars.First<Car>(c => c.Id == carId);
            Cars.Remove(car);
        }
        public decimal GetCarBalance(uint id)
        {
            Car car = Cars.First<Car>(c => c.Id == id);
            return car.Balance;     
        }

        public bool IsIdOfCarExist(uint id)
        {
            Car car = Cars.FirstOrDefault<Car>(c => c.Id == id);
            if(car == null)
            {
                return false;
            }
            return true;
        }

        private void writeTransactionToFile(object obj)
        {
            {
                var lastMinuteTransactins = Settings.Parking.GetLastMinuteTransactions();
                using(StreamWriter log = new StreamWriter("Transactions.log", true, System.Text.Encoding.Default))
                {
                    log.WriteLine("Date and time: {0}", DateTime.Now);
                    decimal sum = 0;
                    foreach (var transaction in lastMinuteTransactins)
                    {
                        sum += transaction.Withdraw;
                    }
                    log.WriteLine("Sum: {0:0.00}", sum);
                }

            }
        }

        private void Timeout(object obj)
        {
            foreach (Car car in Cars)
            {
                decimal price = Settings.prices[car.Type];
                if ((car.Balance - price) < 0)
                {
                    price *= Settings.Fine;
                }
                car.GiveMoney(price);
                AddMoney(price);
                AddTransaction(new Transaction(car.Id, price));
            }
        }

        public int GetNumberOfCars()
        {
            return Cars.Count;
        }

        public IEnumerable<Transaction> GetLastMinuteTransactions()
        {
            var lastMinuteTransactins = Settings.Parking.Transactions.Where<Transaction>(t => DateTime.Now - t.TransactionTime < new TimeSpan(0, 1, 0));
            return lastMinuteTransactins;
        }

        public void Dispose()
        {
            _takeMoney.Dispose();
            _writeToFile.Dispose();
        }
    }
}
