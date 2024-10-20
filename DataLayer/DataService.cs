using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer;
public class DataService : IDataService
{
    public int AddCategory(string name, string description)
    {
        var db = new NorthwindContext();

        int id = db.Categories.Max(x => x.Id) + 1;
        var category = new Category
        {
            Id = id,
            Name = name,
            Description = description
        };

        db.Categories.Add(category);

        db.SaveChanges();

        return category.Id;

    }

    public bool DeleteCategory(int id)
    {
        var db = new NorthwindContext();

        var category = db.Categories.Find(id);

        if (category == null)
        {
            return false;
        }

        db.Categories.Remove(category);

        return db.SaveChanges() > 0;

    }

    public IList<Category> GetCategories()
    {
        var db = new NorthwindContext();
        return db.Categories.ToList();
    }


    public Category GetCategory(int id)
    {
        var db = new NorthwindContext();
        var category = db.Categories.Find(id);
        return category;
    }
    public Category CreateCategory(string name, string description)
    {
        var db = new NorthwindContext();
        int id = db.Categories.Max(x => x.Id) + 1;

        var category = new Category
        {
            Id = id,
            Name = name,
            Description = description
        };

        db.Categories.Add(category);
        db.SaveChanges();

        return category;
    }



    public bool UpdateCategory(int id, string newName, string newDescription)
    {
        var db = new NorthwindContext();

        
        var category = db.Categories.Find(id);

        // If the category is not found, return false
        if (category == null)
        {
            return false;
        }

       
        category.Name = newName;
        category.Description = newDescription;

        
        db.SaveChanges();

        return true;  // Return true indicating the update was successful
    }
    
    public IList<Product> GetProducts()
    {
        var db = new NorthwindContext();
        return db.Products.Include(x => x.Category).ToList();
    }

    public Product GetProduct(int id)
    {
        var db = new NorthwindContext();
        var product = db.Products.Include(p => p.Category).FirstOrDefault(p => p.Id == id);
        return product;
    }
    public IList<Product> GetProductByCategory(int categoryId)
    {
        var db = new NorthwindContext();

        // Retrieve products that belong to the specified category, including category info
        var products = db.Products
                         .Include(p => p.Category)  
                         .Where(p => p.CategoryId == categoryId) 
                         .ToList();

        return products;
    }

    public IList<ProductDto> GetProductByName(string substring)
    {
        var db = new NorthwindContext();

        // Perform a case-insensitive search for products whose names contain the substring
        var products = db.Products
                         .Include(p => p.Category)  // Include the Category navigation property
                         .Where(p => p.Name != null && p.Name.ToLower().Contains(substring.ToLower()))  // Case-insensitive search
                         .Select(p => new ProductDto  // Project to ProductDto
                         {
                             Id = p.Id,
                             ProductName = p.Name,  // Map Name to ProductName
                             CategoryName = p.Category.Name,  // Map Category's Name to CategoryName
                             UnitPrice = p.UnitPrice,
                             QuantityPerUnit = p.QuantityPerUnit,
                             UnitsInStock = p.UnitsInStock
                         })
                         .ToList();

        return products;
    }

    public Order GetOrder(int orderId)
    {
        var db = new NorthwindContext();

        // Fetch the order by Id, including related OrderDetails, Product, and Category
        var order = db.Orders
                      .Include(o => o.OrderDetails)
                      .ThenInclude(od => od.Product)              
                      .ThenInclude(p => p.Category)               
                      .FirstOrDefault(o => o.Id == orderId);      

        return order;
    }

    public IList<Order> GetOrders()
    {
        using var db = new NorthwindContext();

        // Fetch all orders
        var orders = db.Orders
                       .Include(o => o.OrderDetails)  // Optionally include order details
                       .ToList();

        return orders;
    }
    public IList<OrderDetail> GetOrderDetailsByOrderId(int orderId)
    {
        using var db = new NorthwindContext();

        var orderDetails = db.OrderDetails
                             .Include(od => od.Product)
                             .Where(od => od.OrderId == orderId)
                             .ToList();

        return orderDetails;
    }

    public IList<OrderDetail> GetOrderDetailsByProductId(int productId)
    {
        using var db = new NorthwindContext();

        // Query the order details, include related Order, and order by OrderId
        var orderDetails = db.OrderDetails
                             .Include(od => od.Order)  // Include the related Order information
                             .Where(od => od.ProductId == productId)  // Filter by ProductId
                             .OrderBy(od => od.OrderId)  // Order by OrderId to ensure correct first result
                             .ToList();

        return orderDetails;
    }







}
