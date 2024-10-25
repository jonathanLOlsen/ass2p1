using DataLayer;
using Microsoft.AspNetCore.Mvc;

namespace NorthwindWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly IDataService _dataService;

        // Constructor to inject IDataService
        public CategoriesController(IDataService dataService)
        {
            _dataService = dataService;
        }

        // GET: /api/categories
        [HttpGet]
        public IActionResult GetCategories()
        {
            var categories = _dataService.GetCategories();  // Fetch categories from the service
            if (categories == null || !categories.Any())
            {
                return NotFound();  // Return 404 if no categories are found
            }
            return Ok(categories);  // Return 200 OK with the list of categories
        }

        [HttpGet("{id}")]
        public IActionResult GetCategoriById(int id)
        {
            var categori = _dataService.GetCategory(id);
            if (categori == null)
            {
                return NotFound();
            }
            return Ok(categori);
        }
        [HttpPost]
        public IActionResult PostCategories([FromBody] CategoryDto newCategory)
        {
            
            if (string.IsNullOrWhiteSpace(newCategory.Name))
            {
                return BadRequest("Category name is required.");
            }

            
            var categoryId = _dataService.AddCategory(newCategory.Name, newCategory.Description);

            
            var createdCategory = _dataService.GetCategory(categoryId);

            
            return CreatedAtAction(nameof(GetCategoriById), new { id = categoryId }, createdCategory);
        }
        // DELETE: /api/categories/{id}
        [HttpDelete("{id}")]
        public IActionResult DeleteCategory(int id)
        {
            // Attempt to delete the category via the service
            var result = _dataService.DeleteCategory(id);

            if (!result)
            {
                return NotFound();  // If the category doesn't exist, return 404
            }

            return Ok();  // Return 200 OK if
        }
        [HttpPut("{id}")]
        public IActionResult UpdateCategory(int id, [FromBody] CategoryDto updatedCategory)
        {
            // Check if the category exists
            var existingCategory = _dataService.GetCategory(id);
            if (existingCategory == null)
            {
                return NotFound();  // Return 404 if the category does not exist
            }

            // Update the category using the data service
            var isUpdated = _dataService.UpdateCategory(id, updatedCategory.Name, updatedCategory.Description);
            if (isUpdated)
            {
                return Ok();  // Return 200 OK if the update was successful
            }

            // If update failed, return 500
            return StatusCode(500, "An error occurred while updating the category.");
        }



        public class CategoryDto
        {
            public string Name { get; set; }
            public string? Description { get; set; }
        }

    }
}

