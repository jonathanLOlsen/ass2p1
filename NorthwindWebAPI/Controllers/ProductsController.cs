using DataLayer;
using Microsoft.AspNetCore.Mvc;

[Route("api/[controller]")]
[ApiController]
public class ProductsController : ControllerBase
{
    private readonly IDataService _dataService;

    public ProductsController(IDataService dataService)
    {
        _dataService = dataService;
    }

    
    [HttpGet]
    public IActionResult GetProductsByName([FromQuery] string name)
    {
        
        var products = _dataService.GetProductByName(name);

        
        if (products == null || !products.Any())
        {
            return NotFound(new List<ProductDto>());  
        }

        
        return Ok(products);
    }


    [HttpGet("{id}")]
    public IActionResult GetProductById(int id)
    {
        var product = _dataService.GetProduct(id);
        if (product == null)
        {
            return NotFound();
        }
        return Ok(product);
    }

    
    [HttpGet("category/{categoryId}")]
    public IActionResult GetProductsByCategory(int categoryId)
    {
        
        var products = _dataService.GetProductByCategory(categoryId);

        
        if (products == null || !products.Any())
        {
            return NotFound(new List<ProductDto>());  
        }

        
        var productDtos = products.Select(p => new ProductDto
        {
            Id = p.Id,
            Name = p.Name,
            CategoryName = p.Category.Name,
            UnitPrice = (decimal)p.UnitPrice,
            QuantityPerUnit = p.QuantityPerUnit,
            UnitsInStock = p.UnitsInStock
        }).ToList();

        return Ok(productDtos);
    }






    public class ProductDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string CategoryName { get; set; }
        public decimal UnitPrice { get; set; }
        public string QuantityPerUnit { get; set; }
        public int UnitsInStock { get; set; }
    }







}
