using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using API.Entities;
using Microsoft.EntityFrameworkCore;

namespace API.Data;

public class Seed //class
{
    public static async Task SeedUsers(DataContext context) { //metodo

        if (await context.Users.AnyAsync()) return;
        {
            var userDate = await File.ReadAllTextAsync("Data/UserSeedData.json");

            var options = new JsonSerializerOptions{PropertyNameCaseInsensitive = true}; // para deixar o json em camelCase

            var users = JsonSerializer.Deserialize<List<AppUser>>(userDate, options); // para converter o json para uma lista de AppUser

            if (users == null) return;

            foreach (var user in users)
            {
                using var hmac = new HMACSHA512(); // using garante que o HMAC vai ser destruido quando a funcao terminar

                user.UserName = user.UserName.ToLower();
                user.PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes("Pa$$w0rd")); // para criptografar a senha
                user.PasswordSalt = hmac.Key;

                context.Users.Add(user);

            }
        }

        await context.SaveChangesAsync(); // metodo do .net para salvar as alteracoes

    }
}
