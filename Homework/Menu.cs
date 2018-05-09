using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Homework
{
    class Menu
    {//TODO wrong parse exception
        public static bool Exit { get; private set; } = false;
        public static void ChooseCommande()
        {
            Console.WriteLine("Choose commande: ");
            Console.WriteLine("1. Add car");
            Console.WriteLine("2. Add car balance(by id)");
            Console.WriteLine("3. Delete car(by id)");
            Console.WriteLine("4. Output last minute transaction history");
            Console.WriteLine("5. Get earnings");
            Console.WriteLine("6. Get free places");
            Console.WriteLine("7. Output Transactions.log");
            Console.WriteLine("8. Show car balance(by id)");
            Console.WriteLine("9. Exit");
            int commande = Int32.Parse(Console.ReadLine());
            switch (commande)
            {
                case 1:
                    addCar();
                    break;
                case 2:
                    addBalance();
                    break;
                case 3:
                    removeCar();
                    break;
                case 4:
                    showLastMinuteTransactions();
                    break;
                case 5:
                    showParkingBalance();
                    break;
                case 6:
                    showFreeSpaces();
                    break;
                case 7:
                    outputTransactions();
                    break;
                case 8:
                    showCarBalance();
                    break;
                case 9:
                    exit();
                    break;
                default:
                    throw new Exception("Unknown command");

            }

        }
        private static void addCar()
        {
            Console.WriteLine("enter Car Id: ");
            uint id = UInt32.Parse(Console.ReadLine());
            Console.WriteLine("enter type of car(Passenger/Truck/Bus/Motorcycle): ");
            string type = Console.ReadLine();
            Console.WriteLine("Enter car balance: ");
            decimal balance = Decimal.Parse(Console.ReadLine());
            switch (type.ToLower())
            {
                case "passenger":
                    Settings.Parking.AddCar(new Car(id, CarType.Passenger, balance));
                    break;
                case "truck":
                    Settings.Parking.AddCar(new Car(id, CarType.Truck, balance));
                    break;
                case "bus":
                    Settings.Parking.AddCar(new Car(id, CarType.Bus, balance));
                    break;
                case "motorcycle":
                    Settings.Parking.AddCar(new Car(id, CarType.Motorcycle, balance));
                    break;
                default:
                    throw new Exception(); //TODO my own exception
            }
        }
        private static void addBalance()
        {
            Console.WriteLine("enter car id: ");
            uint id = UInt32.Parse(Console.ReadLine());
            Console.WriteLine("enter amount: ");
            decimal amount = Decimal.Parse(Console.ReadLine());
            Settings.Parking.AddCarMoney(id, amount);
            Console.WriteLine("**Added**");

        }

        private static void removeCar()
        {
            Console.WriteLine("enter car id: ");
            uint id = UInt32.Parse(Console.ReadLine());
            if(Settings.Parking.GetCarBalance(id) < 0)
            {
                throw new Exception("You havent enough money");
            }
            Settings.Parking.DeleteCar(id);
            Console.WriteLine("**Removed**");

        }

        private static void showLastMinuteTransactions()
        {
            var lastMinuteTransactins = Settings.Parking.Transactions.
                Where<Transaction>(t => DateTime.Now - t.TransactionTime < new TimeSpan(0, 1, 0));
            foreach (var transaction in lastMinuteTransactins)
            {
                Console.WriteLine(transaction);
            }
        }
        private static void exit()
        {
            Exit = true;
        }

        private static void showFreeSpaces()
        {
            Console.WriteLine("**{0} free**", Settings.ParkingSpace - Settings.Parking.Cars.Count);
        }

        private static void showParkingBalance() => Console.WriteLine(Settings.Parking.Balance);

        private static void showCarBalance()
        {
            Console.WriteLine("enter id: ");
            uint id = UInt32.Parse(Console.ReadLine());
            Console.WriteLine("Balance: {0}", Settings.Parking.GetCarBalance(id));
        }

        private static void outputTransactions()
        {
            using (StreamReader sr = new StreamReader("Transactions.log", System.Text.Encoding.Default))
            {
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    Console.WriteLine(line);
                }
            }
        }
    }
}
