using System.ComponentModel.DataAnnotations;

public class CategoryDTO{

    [Required]
    [MaxLength(50)]
    public string Name{get; set;}
}