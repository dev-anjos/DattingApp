using System;
using API.Entities;
using Microsoft.EntityFrameworkCore;

namespace API.Data;

public class DataContext(DbContextOptions options) : DbContext(options)
{
    //dbset= para setar algo no banco de dados, neste caso o appuser
    // "Users" será o nome da tabela
    public DbSet<AppUser> Users { get; set; }
}
 