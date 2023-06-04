using Microsoft.EntityFrameworkCore;
using VShop.ProductApi.Context;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//para obter a string de conexão
var mySqlConnection = builder.Configuration.GetConnectionString("DefaultConnection");
//em seguida vou utilizar essa string de conexão para defnir meu serviço onde vou registrar meu contexto, tem outras formas além dessa que vou fazer

builder.Services.AddDbContext<AppDbContext>(options =>
                  options.UseMySql(mySqlConnection, 
                    ServerVersion.AutoDetect(mySqlConnection)));

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies()); //garanto que tenho todos os assemblies do contexto que vai incluir mapeamento

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
