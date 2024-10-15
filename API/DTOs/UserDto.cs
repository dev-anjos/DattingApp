using System;

namespace API.DTOs;


//DATA TRANSFER OBJECT = Utilizado para transferir dados entre camadas.
public class UserDto
{
    public required string Username { get; set; }
    public required string Token { get; set; }
}
