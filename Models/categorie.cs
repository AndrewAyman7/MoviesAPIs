using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class Categorie {
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)] // 3shan el tinyInt msh btt7wl fe el SQL le 1,2,3....
    public byte Id {get; set;}  // byte: 1->255 , el byte btt7wl fe el SQL le tinyint


    //[Required]  msh m7tag aktbha 3shan by default hatb2a required, 3shan file el csproj m3mol feeh <Nullable>enable</Nullable>

    [MaxLength(50)]
    public required string Name{get; set;}
}