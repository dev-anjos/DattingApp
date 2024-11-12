
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Entities;

//classe para armazenar fotos de perfil no banco de dados
// como  a entities já esta no AppUser, so precisamos colocar 
//o nome da tabela no DBset no DataContext 
[Table("Photos")]        
public class Photo
{
    public int Id { get; set; }
    public required string Url { get; set; }
    public bool IsMain { get; set; } // para saber se essa foto eh a principal
    public string? PublicId { get; set; } //opcional pois ainda nao podemos fazer isso

    // Navigation properties
    public int AppUserId { get; set; }
    public AppUser AppUser { get; set; } = null!;
}