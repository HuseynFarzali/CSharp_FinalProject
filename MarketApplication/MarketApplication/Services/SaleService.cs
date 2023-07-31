using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MarketApplication.Data.Concrete.Models;

namespace MarketApplication.Services
{
    /// <class name="SaleService">
    /// A service class designed to make connections between user and the market object concerning the sale operations via Console.
    /// </class>
    public class SaleService
    {
        // Inheriting the field of general class MarketService to synchronize product-service and sale-service.
        public static readonly Market Market = MarketService.market;

        // Methods below are all functions that prompt user for input via console and conduct appropriate action on market object.
        public static void AddSale()
        {
            try
            {
                Console.Write("Enter the sale date [format: MM/dd/yyyy]: ");
                DateTime date = DateTime.ParseExact(Console.ReadLine(), "MM/dd/yyyy", null);

                Console.Write("How many products do you want to purchase?:  ");
                int number = int.Parse(Console.ReadLine());

                SaleItem[] boughtItems = new SaleItem[number];

                for (int i = 0; i < number; i++)
                {
                    Console.Write($"Enter the ID of the {i + 1}.product: ");
                    int productId = int.Parse(Console.ReadLine());

                    Product product = Market.GetProductById(productId);

                    Console.Write($"Enter the count of this product you want to buy: ");
                    int count = int.Parse(Console.ReadLine());

                    boughtItems[i] = new SaleItem(product, count);
                }

                Sale sale = new Sale(date, boughtItems);
                Market.AddSale(sale);

                Console.WriteLine("Sale added to the market sale-list with following specifications:");
                sale.TablePrint();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception: {ex.Message}");
            }
        }
        public static void RefundProductFromSale()
        {
            try
            {
                Console.Write("Enter the sale ID you want to apply for a refund: ");
                int saleId = int.Parse(Console.ReadLine());
                Market.GetSaleById(saleId);

                Console.Write("Enter the product ID from the specified sale you want to refund: ");
                int productId = int.Parse(Console.ReadLine());
                Market.GetProductById(productId);

                if (!Market.SaleList.Any(
                        sale => sale.SaleItems.Any(
                            sItem => sItem.Product.ID == productId)))
                {
                    throw new ArgumentException($"Specified product ID value is not valid for the specified sale ID value, as there is no any product in the sale by the given product and sale ID values.");
                }

                Console.Write("How many of this product do you want to refund: [be sure that this number is less than or equal to the bought quantity]: ");
                int quantityToBeRefunded = int.Parse(Console.ReadLine());

                Market.RefundProduct(saleId, productId, quantityToBeRefunded);
                Console.WriteLine("Refund is successful. New sale specifications:");
                Market.GetSaleById(saleId).TablePrint();

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception: {ex.Message}");
            }
        }
        public static void DeleteSale()
        {
            try
            {
                Console.Write("Enter the ID of the sale that you want to refund/remove entirely: ");
                int saleId = int.Parse(Console.ReadLine());

                Market.RefundEntireSale(saleId);
                Console.WriteLine("Sale deleted successfully.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception: {ex.Message}");
            }
        }
        public static void ShowAllSales()
        {
            try
            {
                var sales = Market.SaleList;

                if (!sales.Any())
                    throw new ArgumentException("There is no sale to be shown in the sale-list of the market.");

                foreach (var sale in sales)
                {
                    sale.TablePrint();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception: {ex.Message}");
            }
        }
        public static void ShowSalesByDateRange()
        {
            try
            {
                Console.Write("Enter the start date [format: MM/dd/yyyy]: ");
                DateTime startDate = DateTime.ParseExact(Console.ReadLine(), "MM/dd/yyyy", null);

                Console.Write("Enter the end date [format: MM/dd/yyyy]: ");
                DateTime endDate = DateTime.ParseExact(Console.ReadLine(), "MM/dd/yyyy", null);

                var sales = Market.GetSalesByCriteria(
                    sale => sale.Date >= startDate && sale.Date <= endDate);

                foreach (var sale in sales)
                {
                    sale.TablePrint();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception: {ex.Message}");
            }
        }
        public static void ShowSalesByPriceRange()
        {
            try
            {
                Console.Write("Enter the start price: ");
                decimal startPrice = decimal.Parse(Console.ReadLine());

                Console.Write("Enter the end price: ");
                decimal endPrice = decimal.Parse(Console.ReadLine());

                var sales = Market.GetSalesByCriteria(
                    sale => sale.Price >= startPrice && sale.Price <= endPrice);

                foreach (var sale in sales)
                {
                    sale.TablePrint();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception: {ex.Message}");
            }
        }
        public static void ShowSalesByExactDate()
        {
            try
            {
                Console.Write("Enter the date [format MM/dd/yyyy]: ");
                DateTime date = DateTime.ParseExact(Console.ReadLine(), "MM/dd/yyyy", null);

                var sales = Market.GetSalesByCriteria(sale =>
                    sale.Date.Year == date.Year &&
                    sale.Date.Month == date.Month &&
                    sale.Date.Day == date.Day);

                foreach (var sale in sales)
                {
                    sale.TablePrint();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception: {ex.Message}");
            }
        }
        public static void ShowSaleInfoByID()
        {
            try
            {
                Console.Write("Enter the ID of the sale to be searched: ");
                int id = int.Parse(Console.ReadLine());

                Sale sale = Market.GetSaleById(id);

                sale.TablePrint();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception: {ex.Message}");
                throw;
            }
        }
    }
}
