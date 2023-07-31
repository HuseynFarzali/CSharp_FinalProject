using MarketApplication.Services;

namespace MarketApplication
{ 
    internal class Program
    {
        static void Main()
        {
            Console.Clear();
            int option;

            do
            {
                Console.WriteLine();
                Console.WriteLine("1. Enter into Product Submenu.");
                Console.WriteLine("2. Enter into Sales Submenu.");
                Console.WriteLine("0. Exit");

                Console.WriteLine("----------------------------------------------------------------");

                Console.Write("Enter an option: ");

                while (!int.TryParse(Console.ReadLine(), out option))
                {
                    Console.Write("Please enter a valid option: ");
                }

                switch (option)
                {
                    case 1:
                        MarketService.ProductServiceMenu();
                        break;
                    case 2:
                        MarketService.SaleServiceMenu();
                        break;
                    default:
                        Console.WriteLine("No such option.");
                        break;
                }
            }
            while (option != 0);
        }
    }
} 