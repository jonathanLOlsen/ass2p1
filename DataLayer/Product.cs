using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer;
public class Product
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public float UnitPrice {  get; set; }

    public string? QuantityPerUnit { get; set; }

    public int UnitsInStock {  get; set; }


    public int CategoryId { get; set; }
    public Category Category { get; set; }
    // Computed property that returns the Category's name
    public string? CategoryName => Category?.Name;
}
