
using Microsoft.EntityFrameworkCore;

public class CategoriesService : ICategoriesService
{

// Lazem a3ml nos5a mn el DBContext w a3mlha Dependency Injection
    private readonly ApplicationDBContext _context;
    public CategoriesService(ApplicationDBContext context)
    {
        _context = context;
    }    
    
    public async Task<Categorie> Add(Categorie category)
    {
        await _context.Categories.AddAsync(category); // or: await _context.AddAsync(cat); // w hwa hyfhm en el cat de lel categories
        _context.SaveChanges();

        return category;
    }

    public Categorie Delete(Categorie category)
    {
        _context.Remove(category);
        _context.SaveChanges();
        return category;
    }

    public async Task<Categorie> GetCategorById(byte id)
    {
        return await _context.Categories.SingleOrDefaultAsync(c=>c.Id == id);
    }

    public async Task<IEnumerable<Categorie>> GetCategories()
    {
        return await _context.Categories.OrderBy(cat=>cat.Name).ToListAsync();
    }

    public Categorie Update(Categorie category) // public async Task<Categorie> Update(Categorie category) // Not Async
    {
        _context.Update(category);
        _context.SaveChanges();
        return category;
    }
}