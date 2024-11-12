namespace API.DTOs;

public class MemberDto
{
    public int Id { get; set; }
    public string? Username { get; set; }
    // public byte[] PasswordHash { get; set; } = [];
    // public byte[] PasswordSalt { get; set; } = [];
    public int Age { get; set; }   
    public string? PhotoUrl { get; set; }
    public string? KnownAs { get; set; }
    public DateTime Created { get; set; }
    public DateTime LastActive { get; set; }
    public string? Gender { get; set; } //sexo do usuario
    public string? Introduction { get; set; } // "?" faz com que o campo seja opcional
    public string? Interests { get; set; }
    public string? LookingFor { get; set; }
    public  string? City { get; set; }
    public string? Country { get; set; }
    public List<PhotoDto>? Photos { get; set; }
}
