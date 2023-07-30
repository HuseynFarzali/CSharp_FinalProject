using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MarketApplication.Data.Concrete.Models;

namespace MarketApplication.Services
{
    public class ProductService
    {
        public static readonly Market Market = MarketService.market;

        public static void AddProduct()
        {
            try
            {
                Console.Write("Enter the product name: ");
                string name = Console.ReadLine();

                if (string.IsNullOrWhiteSpace(name))
                    throw new ArgumentException("Name parameter of the product cannot be whitespace.");

                name = name.Trim();

                Console.Write("Enter the product value: ");
                decimal value = decimal.Parse(Console.ReadLine());

                if (value <= 0)
                    throw new ArgumentException($"Cannot assign the value {value} as a valid value.");

                Console.Write($"Enter the quantity of the product that should be stocked in the market storage: ");
                int quantity = int.Parse(Console.ReadLine());

                if (value <= 0)
                    throw new ArgumentException($"Cannot assign the value {value} as a valid quantity.");

                Console.Write($"Enter the category of the product: ");
                string enumNames = "Available categories are ";


                Console.WriteLine();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception: {ex.Message}");
            }
        }
        /*
        public static void UpdateProduct();

        public static void DeleteProduct();

        public static void ShowAllProducts();

        public static void ShowProductsByCategory();

        public static void ShowProductsByValueRange();

        public static void ShowProductsByName();*/
    }
}