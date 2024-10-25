using System;
using System.Collections.Generic;

namespace DataLayer
{
    public interface IDataService
    {
        // Category methods
        IList<Category> GetCategories();
        Category GetCategory(int id);
        Category CreateCategory(string name, string description);
        int AddCategory(string name, string description);
        bool DeleteCategory(int id);
        bool UpdateCategory(int id, string newName, string newDescription);

        // Product methods
        IList<Product> GetProducts();
        Product GetProduct(int id);
        IList<Product> GetProductByCategory(int categoryId);
        IList<ProductDto> GetProductByName(string substring);

        // Order methods
        Order GetOrder(int orderId);
        IList<Order> GetOrders();

        // OrderDetail methods
        IList<OrderDetail> GetOrderDetailsByOrderId(int orderId);
        IList<OrderDetail> GetOrderDetailsByProductId(int productId);
    }
}
