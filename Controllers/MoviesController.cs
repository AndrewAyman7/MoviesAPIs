using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[Route("api/[controller]")]
[ApiController]
public class MoviesController: ControllerBase{
    private readonly ApplicationDBContext _context;
    public MoviesController(ApplicationDBContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllMovies(){
        var movies = await _context.Movies
        .OrderByDescending(m=>m.Rate)
        .Include(m=>m.Category)
        .Select(m=>new ReturnMovieDTO{
            Id = m.Id,
            Title = m.Title,
            Rate = m.Rate,
            Year = m.Year,
            StoreLine = m.StoreLine,
            CategoryId = m.CategoryId,
            CategoryName = m.Category.Name,
            Poster = m.Poster
        })
        .ToListAsync();
        return Ok(movies);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetMovieById(int id)
    {
        var movie = await _context.Movies.FindAsync(id);
        if(movie == null)  return NotFound();
        return Ok(movie);
    }

    [HttpGet("category/{id}")]
    public async Task<IActionResult> GetMoviesByCategoryId(byte id)
    {
        var movies = await _context.Movies.Where(m=>m.CategoryId==id).ToListAsync(); // FindAsync(id); msh btsht8l m3 where w include
        if(movies.Count == 0)  return NotFound();
        return Ok(movies);
    }


    //----------------------- POST Movie -------------------------//
    // Handle Img Size & Extension
    private new List<string> _allowedFiles = new List<string> {".jpg" , ".png"};
    private long _maxAllowedSize = 1048576; // 1mb = 1024*1024

    [HttpPost]
    public async Task<IActionResult> AddNewMovie([FromForm] MovieDTO dto){
        if(dto.Poster == null)  return BadRequest("poster is required");
        
        // Check Img
        if(!_allowedFiles.Contains(Path.GetExtension(dto.Poster.FileName).ToLower()))  return BadRequest("only jpg or png");
        if(dto.Poster.Length > _maxAllowedSize)  return BadRequest("pls choose img smaller than 1mb");

        var isValidCategory = await _context.Categories.AnyAsync(c=>c.Id == dto.CategoryId); // msh lazem t3ml find 
        if(!isValidCategory)  return BadRequest("Not Valid Category");

        using var dataStream = new MemoryStream();
        await dto.Poster.CopyToAsync(dataStream); // Asynchronously copies the contents of the uploaded file to the target stream.

        var movie = new Movie {
            CategoryId = dto.CategoryId,
            Title = dto.Title,
            Rate = dto.Rate,
            Year = dto.Year,
            StoreLine = dto.StoreLine,
            Poster = dataStream.ToArray()
        };

        await _context.AddAsync(movie);
        _context.SaveChanges();

        return Ok(movie);
    }

    [HttpDelete("{id}")]    
    public async Task<IActionResult> DeleteMovie(int id)
    {
        var movie = await _context.Movies.FindAsync(id);
        if(movie == null)  return NotFound();

        _context.Movies.Remove(movie); // or _context.Remove(movie);
        _context.SaveChanges();
        return Ok(movie);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateMovie(int id, [FromForm] MovieDTO dto){
        // 1- Checks
        var movie = await _context.Movies.FindAsync(id);
        if(movie == null)  return NotFound();

        var isValidCategory = await _context.Categories.AnyAsync(c=>c.Id == dto.CategoryId);
        if(!isValidCategory)  return BadRequest("Not Valid Category");

        if(dto.Poster != null){
            if(!_allowedFiles.Contains(Path.GetExtension(dto.Poster.FileName).ToLower()))  return BadRequest("only jpg or png");
            if(dto.Poster.Length > _maxAllowedSize)  return BadRequest("pls choose img smaller than 1mb");

            using var dataStream = new MemoryStream();
            await dto.Poster.CopyToAsync(dataStream); 

            movie.Poster = dataStream.ToArray();
        }

        // 2- Logic
        movie.Title = dto.Title;
        movie.Rate = dto.Rate;
        movie.Year = dto.Year;
        movie.StoreLine = dto.StoreLine;
        movie.CategoryId = dto.CategoryId;

        _context.SaveChanges();
        return Ok(movie);

    }

}
