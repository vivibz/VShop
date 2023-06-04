using Microsoft.EntityFrameworkCore;
using VShop.ProductApi.Context;
using VShop.ProductApi.Models;

namespace VShop.ProductApi.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly AppDbContext _context;

        public ProductRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Product>> GetAll()  
        {
            return await _context.Products.ToListAsync();
        }
        public async Task<IEnumerable<Product>> GetCategoriesProducts()
        {
            return await _context.Products.Include(c => c.Category).ToListAsync();
        }
        public async Task<Product> GetById(int id)
        {
            //abaixo eu quero filtrar as categorias cujo Id é igual ao Id que estou recebendo
            return await _context.Products.Where(p => p.Id == id).FirstOrDefaultAsync();
        }
        public async Task<Product> Create(Product product)
        {
            _context.Products.Add(product); //Vou incluir o objeto que estou recebendo noo contexto, na memória.
            await _context.SaveChangesAsync(); //O repositório pode ou não ter um método para salvar, caso seja rigoroso não pode.O correto seria implemntar o padrão Unit of work e esse SaveChanges dar o commit no Unit of Work
            return product;
        }
        public async Task<Product> Update(Product product)
        {
            _context.Entry(product).State = EntityState.Modified; //O estamos indicando que o estado da entidade é modificado e com isso o Entity...vai entender que teve uma alteraçao nesse objeto e ele vai salvar esse objeto
            await _context.SaveChangesAsync();
            return product;
        }
        public async Task<Product> Delete(int id)
        {
            var product = await GetById(id);
            _context.Products.Remove(product); //remover esse objeto do contexto
            await _context.SaveChangesAsync();
            return product;
        }
    }
}
