
using API.Extensions;

namespace API.Entities;

public class AppUser
{
    public int Id { get; set; }
    public required string UserName { get; set; }
    public byte[] PasswordHash { get; set; } = [];
    public byte[] PasswordSalt { get; set; } = [];
    public  DateOnly DateOfBirth { get; set; }
    public required string KnownAs { get; set; }
    public DateTime Created { get; set; } = DateTime.UtcNow; //Data de criação do usuario
    public DateTime LastActive { get; set; } = DateTime.UtcNow; //Data de atualização do usuario
    public required string Gender { get; set; } //sexo do usuario
    public string? Introduction { get; set; } // "?" faz com que o campo seja opcional
    public string? Interests { get; set; }
    public string? LookingFor { get; set; }
    public required string City { get; set; }
    public required string Country { get; set; }
    public List<Photo> Photos { get; set; } = [];

    // public int GetAge()
    // {
    //     return DateOfBirth.CalculateAge();
    // }
}
