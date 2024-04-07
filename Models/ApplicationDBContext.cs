using Microsoft.EntityFrameworkCore;

public class ApplicationDBContext:DbContext{
    public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options):base(options) // Dependency Injection
    {
        
    }

    public DbSet<Categorie> Categories {get; set;}
    public DbSet<Movie> Movies {get; set;}
}