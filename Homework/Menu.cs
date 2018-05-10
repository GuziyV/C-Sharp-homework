using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Homework
{
    class Menu
    {
        public static bool Exit { get; private set; } = false;
        public static void ChooseCommande()
        {
            Console.WriteLine("Choose commande");
            Console.WriteLine("1. Add car");
            Console.WriteLine("2. Add car balance(by id)");
            Console.WriteLine("3. Delete car(by id)");
            Console.WriteLine("4. Output last minute transaction history");
            Console.WriteLine("5. Show parking profit");
            Console.WriteLine("6. Show last minute profit");
            Console.WriteLine("7. Output Transactions.log");
            Console.WriteLine("8. Show car balance(by id)");
            Console.WriteLine("9. Show number of free places");
            Console.WriteLine("10. Show number of cars in a parking lot");
            Console.WriteLine("11. Exit");
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
                    showLastMinuteEarnings();
                    break;
                case 7:
                    outputTransactions();
                    break;
                case 8:
                    showCarBalance();
                    break;
                case 9:
                    showFreeSpaces(); 
                    break;
                case 10:
                    showNumberOfCars();
                    break;
                case 11:
                    exit();
                    break;
                default:
                    throw new WrongTypeOfCarException("Unknown command");
            }
        }
        private static void addCar()
        {
            Console.WriteLine("enter Car Id: ");
            uint id = UInt32.Parse(Console.ReadLine());
            if (Settings.Parking.IsIdOfCarExist(id))
            {
                throw new WrongCommandException("Id is already exists");
            }
            Console.WriteLine("enter type of car(Passenger/Truck/Bus/Motorcycle): ");
            string type = Console.ReadLine();
            CarType cType;

            switch (type.ToLower())
            {
                case "passenger":
                    cType = CarType.Passenger;
                    break;
                case "truck":
                    cType = CarType.Truck;
                    break;
                case "bus":
                    cType = CarType.Bus;
                    break;
                case "motorcycle":
                    cType = CarType.Motorcycle;
                    break;
                default:
                    throw new WrongTypeOfCarException("Wrong type of car");
            }
            Console.WriteLine("Enter car balance: ");
            decimal balance = Decimal.Parse(Console.ReadLine());
            Settings.Parking.AddCar(new Car(id, cType, balance));
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
            if (Settings.Parking.GetCarBalance(id) < 0)
            {
                throw new NotEnoughMoneyException("You havent enough money");
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

        private static void showCarBalance()
        {
            Console.WriteLine("enter id: ");
            uint id = UInt32.Parse(Console.ReadLine());
            Console.WriteLine("**Balance: {0}**", Settings.Parking.GetCarBalance(id));
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
        private static void showLastMinuteEarnings()
        {
            var lastMinuteTransactins = Settings.Parking.Transactions.
                Where<Transaction>(t => DateTime.Now - t.TransactionTime < new TimeSpan(0, 1, 0));
            decimal sum = 0;
            foreach (var transaction in lastMinuteTransactins)
            {
                sum += transaction.Withdraw;
            }
            Console.WriteLine("**{0}**", sum);
        }
        private static void showNumberOfCars() => Console.WriteLine("**{0}**", Settings.Parking.Cars.Count);

        private static void showParkingBalance() => Console.WriteLine("**{0}**", Settings.Parking.Balance);
    }
}
