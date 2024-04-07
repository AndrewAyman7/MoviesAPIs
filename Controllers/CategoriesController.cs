using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[Route("api/[controller]")] // api/categories
[ApiController]
public class CategoriesController : ControllerBase{
    
    private readonly ICategoriesService _categoryService;
    public CategoriesController(ICategoriesService categoryService)
    {
        _categoryService = categoryService;
    }

    // Basic APIs Code (Before Services and Dependency Injection)
    /*
    [HttpGet] // By Default: http://localhost:5072/api/categories
    public async Task<IActionResult> GetAllCategories(){
        var cats = await _context.Categories.OrderBy(cat=>cat.Name).ToListAsync();
        return Ok(cats);
    }

    [HttpPost]
    public async Task<IActionResult> AddNewCategory( [FromBody] CategoryDTO dto){ // or: ( CategoryDTO dto ) 3ltool
        var cat = new Categorie{Name = dto.Name}; 

        await _context.Categories.AddAsync(cat); // or: await _context.AddAsync(cat); // w hwa hyfhm en el cat de lel categories
        _context.SaveChanges();

        return Ok(cat);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateCategory(int id, [FromBody] CategoryDTO dto){
        var cat = await _context.Categories.SingleOrDefaultAsync(c=>c.Id == id); //lw elId kan Int msh byte: await _context.Categories.FindAsync()
        if(cat == null) return NotFound($"Category Not Found with ID : {id}");

        cat.Name = dto.Name;
        _context.SaveChanges();
        return Ok(cat);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteCategory(int id){
        var cat = await _context.Categories.SingleOrDefaultAsync(c=>c.Id == id);
        if(cat == null) return NotFound($"Category Not Found with ID : {id}");

        _context.Categories.Remove(cat); // OR : _context.Remove(cat);  //hwa hy3rf mn el cat noo3 el Table
        _context.SaveChanges();
        return Ok(cat);
    }
    */

    [HttpGet] // By Default: http://localhost:5072/api/categories
    public async Task<IActionResult> GetAllCategories(){
        var cats = await _categoryService.GetCategories();
        return Ok(cats);
    }

    [HttpPost]
    public async Task<IActionResult> AddNewCategory( [FromBody] CategoryDTO dto){ // or: ( CategoryDTO dto ) 3ltool
        var cat = new Categorie{Name = dto.Name}; 

        await _categoryService.Add(cat);

        return Ok(cat);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateCategory(byte id, [FromBody] CategoryDTO dto){
        var cat = await _categoryService.GetCategorById(id);
        if(cat == null) return NotFound($"Category Not Found with ID : {id}");

        cat.Name = dto.Name;

        _categoryService.Update(cat);
        return Ok(cat);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteCategory(byte id){
        var cat = await _categoryService.GetCategorById(id);
        if(cat == null) return NotFound($"Category Not Found with ID : {id}");

        _categoryService.Delete(cat);
        return Ok(cat);
    }
    
}