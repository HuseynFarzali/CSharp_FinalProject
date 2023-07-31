﻿using ConsoleTables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Schema;

namespace MarketApplication.Data.Concrete.Models
{

    /// <class name="Sale">
    /// Class that represents a real-life sale in a market. Consider a sale as the collection of sale-items in this case.
    /// </class>
    public class Sale
    {
        /// <summary>
        /// Used for auto-generating ID values for each sale (implemented internally, not by user)
        /// </summary>
        private static int counter = 0;

        /// <summary>
        /// Properties of each sale adds additional information about itself. This information consist of unique ID value(generated by ctor), Price(final cost of the sale that the customer should pay to buy), SaleItems(HashSet in the role of collection of sale-items(reason for choosing this type of collection is explained below.)), and date-time info that holds information of when the sale is done.
        /// </summary>
        #region Properties
        public int ID { get; set; }
        public decimal Price { get; set; }
        /// <summary>
        /// HashSet that represents the collection of sale-items. Consider that in a sale, each product has a number besides itself that represents how many of this product are bought. However each of these sale-items in a sale has a unique product, meaning unique sale-items. Then we choose such type of a collection that has no repeating components like a set. .NET framework provides set type of collection as 'HashSet's.
        /// </summary>
        public HashSet<SaleItem> SaleItems { get; set; }
        public DateTime Date { get; set; }
        #endregion

        #region Constructors
        /// <summary>
        /// Constructor that create an object of type 'Sale' with the information about its properties given (collection that represents sale-items, and date)
        /// </summary>
        /// <param name="date">Represents the DateTime when the sale is done in the market</param>
        /// <param name="saleItems">Represents the collection of sale-items that <see langword="abstract"/>sale should contain in itself.(Collection is type of HashSet)</param>
        public Sale(DateTime date, HashSet<SaleItem> saleItems)
        {
            Date = date;
            SaleItems = saleItems;

            foreach (SaleItem item in saleItems)
            {
                Price += item.Product.Value * item.BoughtCount;
            }

            ID = counter++;
        }
        /// <summary>
        /// Constructor (overload of the above-mentioned) that create an object of type 'Sale' with the information about its properties given (collection that represents sale-items, and date)
        /// </summary>
        /// <param name="date">Represents the DateTime when the sale is done in the market</param>
        /// <param name="saleItems">Represents the collection of sale-items that sale should contain in itself.(Collection is an array of sale-items)</param>
        public Sale(DateTime date, params SaleItem[] saleItems)
        {
            Date = date;
            SaleItems = new HashSet<SaleItem>();

            // While iterating through the given array of sale-items, if a sale-item is present in the SaleItems hash-set, we handle situation as we increase the bought-count of the found sale-item in the SaleItems hash-set by the iterator's bought-count.
            foreach (SaleItem item in saleItems)
            {
                SaleItem? matchingItem = SaleItems.FirstOrDefault(
                    sale_item => sale_item.Product.ID == item.Product.ID);

                if (matchingItem != default(SaleItem))
                    matchingItem.BoughtCount += item.BoughtCount;
                else
                    SaleItems.Add(item);

                Price += item.Product.Value * item.BoughtCount;
            }

            ID = counter++;
        }
        #endregion

        #region Methods
        /// <summary>
        /// A simple method that uses a user-made Nuget package that can print information on the console in a table-form.
        /// </summary>
        public void TablePrint()
        {
            var table = new ConsoleTable();
            table.AddColumn(new string[] { "Product ID", "Product Name", "Bought Quantity" });

            Console.WriteLine($"Sale ID: {ID}\tSale Price: {Price}\tSale Date: {$"{Date.Month}/{Date.Day}/{Date.Year}"}");
            foreach (var item in SaleItems)
            {
                table.AddRow(item.Product.ID, item.Product.Name, item.BoughtCount);
            }

            table.Write();
        }
        #endregion
    }
}
