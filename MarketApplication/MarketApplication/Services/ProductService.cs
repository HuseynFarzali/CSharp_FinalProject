using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsoleTables;
using MarketApplication.Data.Concrete.Enumerators;
using MarketApplication.Data.Concrete.Models;

namespace MarketApplication.Services
{
    /// <class name="ProductService">
    /// A service class designed to make connections between user and the market object concerning the product operations via Console.
    /// </class>
    public class ProductService
    {
        // Inheriting the field of general class MarketService to synchronize product-service and sale-service
        public static readonly Market Market = MarketService.market;

        // Methods below are all functions that prompt user for input via console and conduct appropriate action on market object.
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

                if (quantity <= 0)
                    throw new ArgumentException($"Cannot assign the value {value} as a valid quantity.");

                Console.WriteLine($"Enter the category of the product: ");
                Console.Write("(Available categories are ");

                string enumNames = 
                    typeof(ProductCategory).GetEnumNames().Aggregate(
                        (a, b) => a + ", " + b);
                Console.WriteLine(enumNames);

                ProductCategory category = (ProductCategory)Enum.Parse(typeof(ProductCategory), Console.ReadLine());

                Product product = new Product(name, value, quantity, category);
                Market.AddProduct(product);

                Console.WriteLine("Product added to the storage of the market with following specifications:");
                product.TablePrint();

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception: {ex.Message}");
            }
        }
        public static void UpdateProduct()
        {
            try
            {
                Console.Write("Enter the Id product to be updated: ");
                int id = int.Parse(Console.ReadLine());
                Market.GetProductById(id);

                Console.Write("Enter the new product name: ");
                string newName = Console.ReadLine();

                if (string.IsNullOrWhiteSpace(newName))
                    throw new ArgumentException("New name of the product cannot be a whitespace.");

                Console.Write("Etner the new product value: ");
                decimal newValue = decimal.Parse(Console.ReadLine());

                if (newValue <= 0)
                    throw new ArgumentException("New value of the product cannot be non-positive.");

                Console.Write("Enter the new product quantity: ");
                int newQuantity = int.Parse(Console.ReadLine());

                if (newQuantity <= 0)
                    throw new ArgumentException("New quantity of the product cannot be non-positive.");

                Console.Write("Enter the new product category: ");
                ProductCategory newCategory = (ProductCategory)Enum.Parse(typeof(ProductCategory), Console.ReadLine());

                Market.UpdateProduct(
                    oldProductId: id,
                    newName: newName,
                    newValue: newValue,
                    newQuantity: newQuantity,
                    newCategory: newCategory);

                Console.WriteLine("Product information is updated with the following new specifications:");
                Market.GetProductById(id).TablePrint();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception: {ex.Message}");
            }
        }
        public static void DeleteProduct()
        {
            try
            {
                Console.Write("Enter the product ID to be removed: ");
                int id = int.Parse(Console.ReadLine());

                Market.RemoveProduct(id);
                Console.WriteLine("Specified product is removed from the storage.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception: {ex.Message}");
            }
        }
        public static void ShowAllProducts()
        {
            var products = Market.ProductList;

            if (!products.Any())
            {
                Console.WriteLine("There is no product in the market.");
                return;
            }

            var table = new ConsoleTable();

            table.AddColumn(new string[] { "ID", "Name", "Value", "Quantity", "Category" });

            foreach (var product in products)
            {
                table.AddRow(product.ID, product.Name, product.Value, product.Quantity, product.Category);
            }

            table.Write();
        }
        public static void ShowProductsByCategory()
        {
            try
            {
                Console.Write("Enter the category: ");
                ProductCategory category = (ProductCategory)Enum.Parse(typeof(ProductCategory), Console.ReadLine().Trim());

                var foundProducts = Market.GetProductsByCriteria(
                    p => p.Category.Equals(category));

                var table = new ConsoleTable();

                table.AddColumn(new string[] { "ID", "Name", "Value", "Quantity", "Category" });

                foreach (var product in foundProducts)
                {
                    table.AddRow(product.ID, product.Name, product.Value, product.Quantity, product.Category);
                }

                table.Write();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception: {ex.Message}");
            }
        }
        public static void ShowProductsByValueRange()
        {
            try
            {
                Console.Write("Enter the start value: ");
                decimal startValue = decimal.Parse(Console.ReadLine());

                Console.Write("Enter the end value: ");
                decimal endValue = decimal.Parse(Console.ReadLine());

                var foundProducts = Market.GetProductsByCriteria(
                    p => p.Value >= startValue && p.Value <= endValue);

                var table = new ConsoleTable();
                table.AddColumn(new string[] { "ID", "Name", "Value", "Quantity", "Category" });

                foreach (var product in foundProducts)
                {
                    table.AddRow(product.ID, product.Name, product.Value, product.Quantity, product.Category);
                }

                table.Write();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception: {ex.Message}");
            }
        }
        public static void ShowProductsByName()
        {
            try
            {
                Console.Write("Enter the name of product to be searched: ");
                string name = Console.ReadLine();

                if (string.IsNullOrWhiteSpace(name))
                    throw new ArgumentException("Name parameter cannot be whitespace.");

                name = name.Trim();

                var foundProducts = Market.GetProductsByCriteria(
                    p => p.Name == name);

                var table = new ConsoleTable();

                table.AddColumn(new string[] { "ID", "Name", "Value", "Quantity", "Category" });

                foreach (var product in foundProducts)
                {
                    table.AddRow(product.ID, product.Name, product.Value, product.Quantity, product.Category);
                }

                table.Write();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception: {ex.Message}");
                throw;
            }
        }
    }
}