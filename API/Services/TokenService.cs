using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using API.Entities;
using API.Interfaces;
using Microsoft.IdentityModel.Tokens;

namespace API.Services;

public class TokenService(IConfiguration config) : ITokenService
{

    public string CreateToken(AppUser user )
    {
        //?? = Se o valor da esquerda for nulo, ele atribui o valor da direita.
        // tokenkey = chave de token
       var tokenKey = config["TokenKey"] ?? 
       throw new Exception("Cannot access TokenKey from appsettings");

        //verificar se o tokenkey tem pelo menos 64 caracteres
       if(tokenKey.Length < 64 ) throw new Exception("Your tokenkey needs to be longer");
       
        // symmetricSecurityKey = assimetrico uma chave para criptografar e outra para descriptografar, o contrario de simetrico.
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(tokenKey));

        //Claims = declarações do usuario, exemplo:  meu nome é Bob , eu afirmo.
        var claims = new List<Claim>
        {
            // identificador do usuario
            new(ClaimTypes.NameIdentifier, user.Id.ToString()),
        };

        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

        //criando a descricao do token, como um bilete de festa, nome do convidado, data de expiração
        // e carimbo de validade(signingCredentials)
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.Now.AddDays(7),
            SigningCredentials = creds
        };

        // manipulando o token, criando o token e o retornando
        var tokenHandler = new JwtSecurityTokenHandler();
        var token = tokenHandler.CreateToken(tokenDescriptor);

        return tokenHandler.WriteToken(token);
    }

}
