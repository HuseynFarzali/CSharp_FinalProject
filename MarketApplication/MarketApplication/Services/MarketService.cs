using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MarketApplication.Data.Concrete.Models;

namespace MarketApplication.Services
{
    public class MarketService
    {
        public static readonly Market market = new();

        public static void ProductServiceMenu()
        {
            Console.Clear();
            int option;

            do
            {
                Console.WriteLine();
                Console.WriteLine("1. Add New Product.");
                Console.WriteLine("2. Update Existing Product.");
                Console.WriteLine("3. Delete Existing Product.");
                Console.WriteLine("4. Show All Products.");
                Console.WriteLine("5. Show Products of Specified Category.");
                Console.WriteLine("6. Show Products in Range of Value.");
                Console.WriteLine("7. Show Products by Specified Name.");
                Console.WriteLine("0. Return The Main Menu.");
                Console.WriteLine("----------------------------------------------------------------");

                Console.Write("Enter an option: ");
                while (!int.TryParse(Console.ReadLine(), out option))
                {
                    Console.Write("Please enter a valid option: ");
                }

                switch (option)
                {
                    case 1:
                        ProductService.AddProduct();
                        break;
                    case 2:
                        ProductService.UpdateProduct();
                        break;
                    case 3:
                        ProductService.DeleteProduct();
                        break;
                    case 4:
                        ProductService.ShowAllProducts();
                        break;
                    case 5:
                        ProductService.ShowProductsByCategory();
                        break;
                    case 6:
                        ProductService.ShowProductsByValueRange();
                        break;
                    case 7:
                        ProductService.ShowProductsByName();
                        break;
                    case 0:
                        break;

                    default:
                        Console.WriteLine("No such option.");
                        break;
                }

            }
            while (option != 0);
        }
        
        public static void SaleServiceMenu()
        {
            Console.Clear();
            int option;

            do
            {
                Console.WriteLine();
                Console.WriteLine("1. Add New Sale.");
                Console.WriteLine("2. Refund a Product From Specified Sale");
                Console.WriteLine("3. Delete Existing Sale.");
                Console.WriteLine("4. Show All Sales.");
                Console.WriteLine("5. Show Sales in Specified Date Range.");
                Console.WriteLine("6. Show Sales in Specified Price Range");
                Console.WriteLine("7. Show Sales in Exact Specified Date");
                Console.WriteLine("8. Show Sale Info of Specified Sale ID.");
                Console.WriteLine("0. Return The Main Menu.");
                Console.WriteLine("----------------------------------------------------------------");

                Console.Write("Enter an option: ");
                while (!int.TryParse(Console.ReadLine(), out option))
                {
                    Console.Write("Please enter a valid option: ");
                }

                switch (option)
                {
                    case 1:
                        SaleService.AddSale();
                        break;
                    case 2:
                        SaleService.RefundProductFromSale();
                        break;
                    case 3:
                        SaleService.DeleteSale();
                        break;
                    case 4:
                        SaleService.ShowAllSales();
                        break;
                    case 5:
                        SaleService.ShowSalesByDateRange();
                        break;
                    case 6:
                        SaleService.ShowSalesByPriceRange();
                        break;
                    case 7:
                        SaleService.ShowSalesByExactDate();
                        break;
                    case 8:
                        SaleService.ShowSaleInfoByID();
                        break;
                    case 0:
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
