using MarketApplication.Data.Concrete.Enumerators;
using MarketApplication.Data.Concrete.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketApplication.Data.Abstract.Interfaces
{
    public interface IMarket
    {
        public void AddSale(Sale sale);
        public void RefundProduct(int saleId, int productId, int quantityToBeRefunded);
        public void RefundEntireSale(int saleId);
        public List<Sale> GetSalesByCriteria(Predicate<Sale> criteria);
        public Sale GetSaleById(int saleId);


        public void AddProduct(Product product);
        public void UpdateProduct(
            int productId,
            string newName,
            decimal newValue,
            ProductCategory newCategory,
            int newQuantity);
        public List<Product> GetProductsByCriteria(Predicate<Product> criteria);
        public Product GetProductById(int productId);
    }
}
