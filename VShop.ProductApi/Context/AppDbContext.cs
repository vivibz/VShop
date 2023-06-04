using Microsoft.EntityFrameworkCore;
using VShop.ProductApi.Models;

namespace VShop.ProductApi.Context
{
    //classe do entityframework core: DbContext, aqui vai estar a inteligencia para definir o objeto relacional
    //criar contrutor: ctor
    public class AppDbContext: DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { } //passa as configurações de opções para a classe base
        //definir o mapeamento entre as entidades e as tabelas(mapeamento objeto-relacional)
        public DbSet<Category> Categories { get; set; } //A classe clategore vai ser mapeada para tabela categories, entedidade usa no singular a tabela que vai ser gerada usa no plural;
        public DbSet<Product> Products { get; set; }
        // Depois disso ir no appsettings para definir a string de conexão

        //Fluent API
        protected override void OnModelCreating(ModelBuilder modelbuilder)
        {
            //category
            modelbuilder.Entity<Category>().HasKey(c => c.CategoryId);
           
            modelbuilder.Entity<Category>().
                Property(c => c.Name).
                    HasMaxLength(100).
                      IsRequired();

            //Product
            modelbuilder.Entity<Product>().
                Property(c => c.Name).
                    HasMaxLength(100).
                      IsRequired();

            modelbuilder.Entity<Product>().
                Property(c => c.Description).
                    HasMaxLength(255).
                      IsRequired();

            modelbuilder.Entity<Product>().
                Property(c => c.ImageURL).
                    HasMaxLength(255).
                      IsRequired();

            modelbuilder.Entity<Product>().
                Property(c => c.Price).
                 HasPrecision(12, 2);

            modelbuilder.Entity<Category>()
                .HasMany(g => g.Products)
                  .WithOne(c => c.Category)
                    .IsRequired()
                      .OnDelete(DeleteBehavior.Cascade); //quando eu exluir uma categoria os produtos relacionados vão ser excluidos em cascata

            modelbuilder.Entity<Category>().HasData(  //O HasData cria dados inciais se não ouver, populando a tabela category
                new Category
                {
                    CategoryId = 1,
                    Name = "Material Escolar",
                },
                new Category    //é obrigatório informar o CategoryId
                { 
                    CategoryId = 2,
                    Name = "Acessórios",
                }
                );
        }
    }
}
