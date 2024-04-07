using System.ComponentModel.DataAnnotations;

public class MovieDTO {

    [MaxLength(200)]
    public string Title {get; set;}

    public int Year {get; set;}

    public double Rate {get; set;}

    [MaxLength(1000)]
    public string StoreLine {get; set;}

    public IFormFile? Poster {get; set;}

    public byte CategoryId {get; set;} // FK


}