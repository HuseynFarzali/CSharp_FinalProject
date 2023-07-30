using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MarketApplication.Data.Concrete.Enumerators;

namespace MarketApplication.Data.Concrete.Models
{
    /// <class name="Product">
    /// A Simple model representing real-life products used and sold  in markets
    /// </class>
    public class Product
    {
        /// <summary>
        /// Used for auto-generating ID values for each product (implemented internally, not by user)
        /// </summary>
        private static int counter = 0;

        /// <summary>
        /// Properties of each product adds information to itself, as mentioned below, Name, Value (how much is one of this product cost), Category, Quantity (how much is there of this product in the market stock).
        /// </summary>
        #region Properties
        public int ID { get; set; }
        public string Name { get; set; }
        public decimal Value { get; set; }
        public ProductCategory Category { get; set; }
        public int Quantity { get; set; }
        #endregion


        #region Constructors
        /// <summary>
        /// Constructor to create a product in the market storage
        /// </summary>
        /// <param name="productName">String to represent the product name</param>
        /// <param name="productValue">Decimal to represent product value</param>
        /// <param name="productQuantity">Integer to represent how much of this product should exist in the market storage</param>
        /// <param name="productCategory">ProductCategory(enum type) to represent product category</param>
        public Product(
            string productName,
            decimal productValue,
            int productQuantity,
            ProductCategory productCategory)
        {
            Name = productName;
            Value = productValue;
            Category = productCategory;
            Quantity = productQuantity;

            ID = counter++;
        }
        #endregion

        #region Methods
        /// <summary>
        /// A simple and informative method that gives information about a product in a single string. Not available for a user, but for the developer.
        /// </summary>
        /// <returns>String that contains all necessary information about the product</returns>
        public string GetCode() => $"[p:{Name}|v:{Value}|c:{Category}|q:{Quantity}#{ID}]";
        #endregion
    }
}
