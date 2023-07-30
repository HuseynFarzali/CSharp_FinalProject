using MarketApplication.Data.Abstract.Interfaces;
using MarketApplication.Data.Concrete.Enumerators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketApplication.Data.Concrete.Models
{
    /// <class name="Market">
    /// Class that represents a real-life operating object with methods concerning both products in the market storage and sales in market sale history.
    /// </class>
    /// <baseobject name="IMarket">Interface that encapsulates the fundamental methods that operate on both products and sales of a market. Market class implements IMarket interface.</baseclass>
    public class Market : IMarket
    {
        /// <summary>
        /// Collections 'ProductList' and 'SaleList' are in the place of representing storage of market products and sale history of a market, respectively. Each market can have different product-lists and sale-lists.
        /// </summary>
        #region Properties
        public List<Sale> SaleList { get; set; }
        public List<Product> ProductList { get; set; }
        #endregion

        #region Constructors
        /// <summary>
        /// Simple constructor that instantiate the properties of the market that described above.
        /// </summary>
        public Market()
        {
            SaleList = new List<Sale>();
            ProductList = new List<Product>();
        }
        #endregion

        #region Methods on Product Operations
        /// <summary>
        /// Method for adding new product to the market storage
        /// </summary>
        /// <param name="product">Product that should be added to the storage(ProductList)</param>
        /// <returns>The ID value of the added product</returns>
        /// <exception cref="ArgumentException">Exception that thrown in the case of <paramref name="product"/> is null or uninstantiated.</exception>
        public int AddProduct(Product product)
        {
            if (product == null)
                throw new ArgumentException("Parameter 'product' cannot be null to be added to market storage.");

            ProductList.Add(product);
            return product.ID;
        }

        /// <summary>
        /// Method that returns corresponding product from the storage by the given ID value
        /// </summary>
        /// <param name="productId">ID value by which the product from the storage should be returned</param>
        /// <returns>Product that found from the storage by the given ID value</returns>
        /// <exception cref="ArgumentException">Exception that thrown in the case of given ID value does not match by any product in the storage</exception>
        public Product GetProductById(int productId)
        {
            Product? foundProduct = ProductList.FirstOrDefault(p => p.ID == productId);

            return foundProduct 
               ?? throw new ArgumentException($"There is no matching product in the storage by the given ID: {productId}");
        }

        /// <summary>
        /// Method that returns corresponding product collection from the storage in which each product satisfies the given criteria 
        /// </summary>
        /// <param name="criteria">Predicate that a product from the storage should satisft in order to be added to the returned collection of products</param>
        /// <returns>Collection of products from the storage in the form of a list which each product-element satisfy the given criteria.</returns>
        /// <exception cref="ArgumentException">Exception that thrown in the case of no any product from the storage satisfy the given criteria</exception>
        public List<Product> GetProductsByCriteria(Predicate<Product> criteria)
        {
            var foundProducts = from p in ProductList
                                where criteria(p)
                                select p;

            return foundProducts.Any()
                ? foundProducts.ToList()
                : throw new ArgumentException($"There is no matching product in the storage by the given criteria.");
        }

        /// <summary>
        /// Method that changes the properties of a product from the storage (except the ID value) by the given ID value
        /// </summary>
        /// <param name="oldProductId">ID value by which the searching of the product should be done in the storage</param>
        /// <param name="newName">New name string for the product</param>
        /// <param name="newValue">New value decimal for the product</param>
        /// <param name="newCategory">New category(of type ProductCategory) for the product</param>
        /// <param name="newQuantity">New quantity(integer) for the product</param>
        /// <exception cref="ArgumentException">Excption that thrown in the case of no any product found by the given ID value in the storage</exception>
        public void UpdateProduct(
            int oldProductId, string newName, decimal newValue,
            ProductCategory newCategory, int newQuantity)
        {
            Product oldProduct = ProductList.FirstOrDefault(p => p.ID == oldProductId)
                ?? throw new ArgumentException($"There is no matching product in the storage by the given ID: {oldProductId}");

            oldProduct.Name = newName;
            oldProduct.Value = newValue;
            oldProduct.Category = newCategory;
            oldProduct.Quantity = newQuantity;
        }
         
        /// <summary>
        /// Method for removing existing product from the storage
        /// </summary>
        /// <param name="productId">ID value by which the product is selected from the storage for removal</param>
        public void RemoveProduct(int productId)
        {
            Product product = GetProductById(productId);
            ProductList.Remove(product);
        }
        #endregion

        #region Methods on Sale Operations
        /// <summary>
        /// Method for adding a new sale to the market sale history
        /// </summary>
        /// <param name="sale">Object the type of 'Sale' that should be added to the SaleList</param>
        /// <returns>The ID value of <paramref name="sale"/> that newly added to the SaleList of the market</returns>
        /// <exception cref="ArgumentException">Exception that thrown in the case of sale object is null or uninstantiated</exception>
        public int AddSale(Sale sale)
        {
            if (sale == null)
                throw new ArgumentException("Object 'sale' cannot be null.");

            // As iterating through the sale-items of the sale, we decrease the quantity of the product on the storage by the bought-count.
            foreach (SaleItem item in sale.SaleItems)
            {
                item.Product.Quantity -= item.BoughtCount;
            }

            SaleList.Add(sale);
            return sale.ID;
        }

        /// <summary>
        /// Same as the above-mentioned method but takes collection of them instead of single sale object
        /// </summary>
        /// <param name="sales">Array of the sales that should be added to the market sale history</param>
        /// <returns>An array of ID values whose indexes correspond to those of <paramref name="sales"/></returns>
        /// <exception cref="ArgumentException">Exception that thrown in the case of one of the elements of <paramref name="sales"/> is null or uninstantiated</exception>
        public int[] AddSale(params Sale[] sales)
        {
            // Initiation of ID values array of size of sales array
            int[] idValues = new int[sales.Length];
            // Iterator to hold the index value of the array (like in for loop)
            int iterator = 0;

            foreach (Sale sale in sales)
            {
                if (sale == null)
                    throw new ArgumentException("Object 'sale' cannot be null.");

                foreach (SaleItem item in sale.SaleItems)
                {
                    item.Product.Quantity -= item.BoughtCount;
                }

                SaleList.Add(sale);
                // Adding the ID value of the sale to the ID-value-array
                idValues[iterator++] = sale.ID;
            }

            return idValues;
        }

        /// <summary>
        /// Method that returns corresponding sale object from the market sale history by the given ID value
        /// </summary>
        /// <param name="saleId">ID value that the sale searching should be conducted with</param>
        /// <returns></returns>
        /// <exception cref="ArgumentException">Exception that thrown in the case of there is no any matching sale object by the given ID value</exception>
        public Sale GetSaleById(int saleId)
        {
            Sale? foundSale = SaleList.FirstOrDefault(
                s => s.ID == saleId);

            return foundSale
                ?? throw new ArgumentException($"There is no matching sale in the sale-list by the given ID: {saleId}");
        }

        /// <summary>
        /// Method which is the general version of the above-mentioned which also returns a collection of sales which satisfy the given criteria instead of only one
        /// </summary>
        /// <param name="criteria">Search condition that should be checked by each sale object on the sale-list of the market</param>
        /// <returns>Collection of condition-satisfied sales of type 'List'</returns>
        /// <exception cref="ArgumentException">Exception thrown in the case of no any sale object satisfy the given criteria on the market sale-list</exception>
        public List<Sale> GetSalesByCriteria(Predicate<Sale> criteria)
        {
            var foundSales = from s in SaleList
                             where criteria(s)
                             select s;

            return foundSales.Any()
                ? foundSales.ToList()
                : throw new ArgumentException("There is no matching sale in the sale-list by the given criteria.");
        }

        /// <summary>
        /// Method for refunding operation. From specified sale, method refunds specified amount of specified product that is present on the given sale
        /// </summary>
        /// <param name="saleId">ID value that represents the sale object on which the refund process is conducted</param>
        /// <param name="productId">ID value of the product on the specified sale that should be refunded</param>
        /// <param name="quantityToBeRefunded">Quantity that represents how many of the product is wanted to be refunded</param>
        /// <exception cref="ArgumentException">Exception that thrown in some exceptional cases including if there is no sale by the given <paramref name="saleId"/> parameter, if there is no product by the given <paramref name="productId"/> parameter, <paramref name="quantityToBeRefunded"/> parameter is larger than the quantity of the product that is present in the storage of the marker, and if <paramref name="quantityToBeRefunded"/> parameter is non-positive</exception>
        public void RefundProduct(int saleId, int productId, int quantityToBeRefunded)
        {
            Sale foundSale = SaleList.FirstOrDefault(s => s.ID == saleId)
                ?? throw new ArgumentException($"There is no matching sale in the sale-list by the given ID: {saleId}");

            Product foundProduct = ProductList.FirstOrDefault(p => p.ID == productId)
                ?? throw new ArgumentException($"There is no matching product in the product-list by the given ID: {productId}");

            // Bool parameter that represents the case if the refund operation is a success or not. We initialize the parameter to be false.
            bool flag = false;

            // We iterate through each sale-item of the sale, to find if the sale-item product is the product that wanted to be refunded
            foreach (SaleItem saleItem in foundSale.SaleItems)
            {
                if (saleItem.Product == foundProduct)
                {
                    // As we found the product that wanted to be refunded present in the sale, we check if the given parameter 'quantityToBeRefunded' is valid for refund operation or not.
                    if (quantityToBeRefunded > saleItem.BoughtCount || quantityToBeRefunded <= 0)
                        throw new ArgumentException($"Cannot refund {quantityToBeRefunded} of product:{foundProduct.Name}#{foundProduct.ID} which {saleItem.BoughtCount} of them had been sold");

                    // If the above-case is valid, we update the storage of the market and sale information meaning we increase the product quantity in the storage by the refunded count, decrease the bought-count of the product in the sale, and update the price of the sale.
                    foundProduct.Quantity += quantityToBeRefunded;
                    saleItem.BoughtCount -= quantityToBeRefunded;
                    foundSale.Price -= quantityToBeRefunded * foundProduct.Value;
                    // We finish the process by marking that the refund operation is done successfully, by setting 'flag' variable to true and breaking since further analyzing is unnecessary.
                    flag = true; break;
                }
            }

            // We check if the refund operation is finished successfully, otherwise we throw an ArgumentException.
            if (!flag)
                throw new ArgumentException($"There is no product: {foundProduct.GetCode()}#{foundProduct.ID} sold in the given sale: sale-#{foundSale.ID}");
        }

        /// <summary>
        /// Method that is similar to the above-mentioned, yet more simple which conducts the refund process of an entire sale
        /// </summary>
        /// <param name="saleId">ID value for the selecting the sale to be refunded from market sale-list</param>
        /// <exception cref="ArgumentException">Exception that thrown in case of no any sale object found by the given ID value</exception>
        public void RefundEntireSale(int saleId)
        {
            Sale foundSale = SaleList.FirstOrDefault(s => s.ID == saleId)
                ?? throw new ArgumentException($"There is no matching sale in the sale-list by the given ID: {saleId}");

            // We increase the quantity of each product which is bought in the sale, one-by-one.
            foreach (SaleItem item in foundSale.SaleItems)
            {
                item.Product.Quantity += item.BoughtCount;
            }

            // We remove the sale from the sale history of market.
            SaleList.Remove(foundSale);
        }
        #endregion
    }
}
