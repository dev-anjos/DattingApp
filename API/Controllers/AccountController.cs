using System;
using System.Security.Cryptography;
using System.Text;
using API.Data;
using API.DTOs;
using API.Entities;
using API.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace API.Controllers;

// deve ser passado no construtor o DataContext do banco de dados
// na url, é indificado apenas o "Account" do AccountController
public class AccountController(DataContext context,ITokenService tokenService) : BaseApiController
{

    /// <summary>
    /// Realiza a task de registro, realizando um ação que esperar como retorno um objeto do tipo AppUser
    /// A task recebe username e password como parametro para ser trabalhada e retornada (registerDto)
    /// </summary>
    /// <param name="registerDto">Objeto do tipo RegisterDto com o username e password</param>
    /// <returns>Se estiver tudo correto, retorna o usuario, caso contrario, retorna BadRequest.</returns>
    [HttpPost("register")] //account/register

    //realiza a task de registro, realizando um ação que esperar como retorno um objeto do tipo AppUser
    // A task recebe username e password como parametro para ser trabalhada e retornada (registerDto)
    public async Task<ActionResult<UserDto>> Register(RegisterDto registerDto)
    {

        //verifica se o usuario ja existe no banco de dados
        if (registerDto.Username == "" || registerDto.Password == "") 
         return BadRequest("Nome de usuario e senha devem ser informados")
        ;

        if (await UserExists(registerDto.Username)) 
         return BadRequest("Nome de usuario já utilizado")
        ;

        return Ok();

        // usando o hmac para criptografar a senha
        // using var hmac = new HMACSHA512();

        // var user = new AppUser
        // {
        //     UserName = registerDto.Username.ToLower(),
        //     PasswordHash = 
        //     hmac.ComputeHash(Encoding.UTF8.GetBytes(registerDto.Password)),
        //     PasswordSalt = hmac.Key
        // };

        // // adicionando o usuario no contexto(banco de dados) de forma assincrona.
        // context.Users.Add(user);
        // await context.SaveChangesAsync();

        // return new UserDto
        // {
        //     Username = user.UserName,
        //     Token = tokenService.CreateToken(user)
        // };
    }

    /// <summary>
    /// Realiza a task de login, verificando se o usuario existe, 
    /// se a senha esta correta e retorna o usuario.
    /// </summary>
    /// <param name="loginDto">Objeto do tipo LoginDto com o username e password</param>
    /// <returns>Se estiver tudo correto, retorna o usuario, caso contrario, retorna Unauthorized.</returns>
    [HttpPost("login")]//account/login
    public async Task<ActionResult<UserDto>> Login(LoginDto loginDto)
    {
        var user = await context.Users.FirstOrDefaultAsync
            (x => x.UserName == loginDto.UserName.ToLower())
        ;

        if (user == null) return Unauthorized("Nome de usuario inválido");
        using var hmac = new HMACSHA512(user.PasswordSalt);

        var computeHash = 
            hmac.ComputeHash(Encoding.UTF8.GetBytes(loginDto.Password))
        ;

        for (int i = 0; i < computeHash.Length; i++){
            if (computeHash[i] != user.PasswordHash[i]) 
            return Unauthorized("Senha inválida");
        }

        return new UserDto
        {
            Username = user.UserName,
            Token = tokenService.CreateToken(user)
        };
    }

    //bool = boleano
    private async Task<bool> UserExists(string username)
    {
        return await context.Users.AnyAsync
            (x => x.UserName.ToLower() == username.ToLower())
        ;
    }
}
