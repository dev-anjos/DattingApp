using API.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddDbContext<DataContext>( opt => 
{
    // informando que vamos usar o sqlite | getconnectionstring = pegando a string de conexão
    //DefaultConnection = nome da string de conexaão
    opt.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection"));
});
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddCors();

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseCors(x => x.AllowAnyHeader().AllowAnyHeader()
    .WithOrigins("http://localhost:4200" , "https://localhost:4200"));
    
app.MapControllers();

app.Run();
