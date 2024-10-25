using DataLayer;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class DataService : IDataService
{
    private readonly NorthwindContext _context;

    // Inject the NorthwindContext via constructor
    public DataService(NorthwindContext context)
    {
        _context = context;
    }
    // Parameterless constructor for testing purposes
    public DataService()
    {
        _context = new NorthwindContext(new DbContextOptionsBuilder<NorthwindContext>()
            .UseNpgsql("Host=localhost;Database=northwind;Username=postgres;Password=Jolsen123")
            .Options);
    }

    public int AddCategory(string name, string description)
    {
        int id = _context.Categories.Max(x => x.Id) + 1;
        var category = new Category
        {
            Id = id,
            Name = name,
            Description = description
        };

        _context.Categories.Add(category);
        _context.SaveChanges();

        return category.Id;
    }

    public bool DeleteCategory(int id)
    {
        var category = _context.Categories.Find(id);

        if (category == null)
        {
            return false;
        }

        _context.Categories.Remove(category);
        return _context.SaveChanges() > 0;
    }

    public IList<Category> GetCategories()
    {
        return _context.Categories.ToList();
    }

    public Category GetCategory(int id)
    {
        return _context.Categories.Find(id);
    }

    public Category CreateCategory(string name, string description)
    {
        int id = _context.Categories.Max(x => x.Id) + 1;

        var category = new Category
        {
            Id = id,
            Name = name,
            Description = description
        };

        _context.Categories.Add(category);
        _context.SaveChanges();

        return category;
    }

    public bool UpdateCategory(int id, string newName, string newDescription)
    {
        var category = _context.Categories.Find(id);

        if (category == null)
        {
            return false;
        }

        category.Name = newName;
        category.Description = newDescription;

        _context.SaveChanges();
        return true;
    }

    public IList<Product> GetProducts()
    {
        return _context.Products.Include(x => x.Category).ToList();
    }

    public Product GetProduct(int id)
    {
        return _context.Products.Include(p => p.Category).FirstOrDefault(p => p.Id == id);
    }

    public IList<Product> GetProductByCategory(int categoryId)
    {
        return _context.Products
                       .Include(p => p.Category)
                       .Where(p => p.CategoryId == categoryId)
                       .ToList();
    }

    public IList<ProductDto> GetProductByName(string substring)
    {
        return _context.Products
                       .Include(p => p.Category)
                       .Where(p => p.Name != null && p.Name.ToLower().Contains(substring.ToLower()))
                       .Select(p => new ProductDto
                       {
                           Id = p.Id,
                           ProductName = p.Name,
                           CategoryName = p.Category.Name,
                           UnitPrice = p.UnitPrice,
                           QuantityPerUnit = p.QuantityPerUnit,
                           UnitsInStock = p.UnitsInStock
                       })
                       .ToList();
    }

    public Order GetOrder(int orderId)
    {
        return _context.Orders
                       .Include(o => o.OrderDetails)
                       .ThenInclude(od => od.Product)
                       .ThenInclude(p => p.Category)
                       .FirstOrDefault(o => o.Id == orderId);
    }

    public IList<Order> GetOrders()
    {
        return _context.Orders.Include(o => o.OrderDetails).ToList();
    }

    public IList<OrderDetail> GetOrderDetailsByOrderId(int orderId)
    {
        return _context.OrderDetails
                       .Include(od => od.Product)
                       .Where(od => od.OrderId == orderId)
                       .ToList();
    }

    public IList<OrderDetail> GetOrderDetailsByProductId(int productId)
    {
        return _context.OrderDetails
                       .Include(od => od.Order)
                       .Where(od => od.ProductId == productId)
                       .OrderBy(od => od.OrderId)
                       .ToList();
    }
}
