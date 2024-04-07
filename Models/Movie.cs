using System.ComponentModel.DataAnnotations;

public class Movie {
    public int Id {get; set;}

    [MaxLength(200)]
    public string Title {get; set;}

    public int Year {get; set;}

    public double Rate {get; set;}

    [MaxLength(1000)]
    public string StoreLine {get; set;}

    public byte[] Poster {get; set;}

    public byte CategoryId {get; set;} // FK

    public Categorie Category {get; set;} // Navigation Prop


}