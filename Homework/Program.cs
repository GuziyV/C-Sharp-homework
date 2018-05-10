using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Homework
{
    class Program
    {
        static void Main(string[] args)
        {
            while (Menu.Exit == false)
            {
                try
                {
                    Menu.ChooseCommande();
                }
                
                catch (WrongCommandException ex)
                {
                    Console.WriteLine("**{0}**", ex.Message);
                }
                catch (WrongTypeOfCarException ex)
                {
                    Console.WriteLine("**{0}**", ex.Message);
                }
                catch(NotEnoughMoneyException ex)
                {
                    Console.WriteLine("**{0}**", ex.Message);
                }
                catch (NotEnoughSpaceException ex)
                {
                    Console.WriteLine("**{0}**", ex.Message);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("**{0}**", ex.Message);
                }
                finally
                {
                    Console.WriteLine(new string('_', 10));
                }
            }

        }
    }
}
