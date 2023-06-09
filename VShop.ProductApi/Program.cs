using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;
using VShop.ProductApi.Context;
using VShop.ProductApi.Repositories;
using VShop.ProductApi.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers().AddJsonOptions(x =>
                 x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//para obter a string de conex�o
var mySqlConnection = builder.Configuration.GetConnectionString("DefaultConnection");
//em seguida vou utilizar essa string de conex�o para defnir meu servi�o onde vou registrar meu contexto, tem outras formas al�m dessa que vou fazer

builder.Services.AddDbContext<AppDbContext>(options =>
                  options.UseMySql(mySqlConnection, 
                    ServerVersion.AutoDetect(mySqlConnection)));

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies()); //garanto que tenho todos os assemblies do contexto que vai incluir mapeamento

builder.Services.AddScoped<ICategoryRepository, CategoryRepository>(); // se n�o fizer isso, n�o estarei adicionando o servi�o no containerDI
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<IProductService, ProductService>();

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
