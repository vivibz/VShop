using Microsoft.EntityFrameworkCore;
using VShop.ProductApi.Context;
using VShop.ProductApi.Models;

namespace VShop.ProductApi.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        
        private readonly AppDbContext _context; //O repositório tem que acessar o banco de dados usando o contexto do entity framework core

        public CategoryRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Category>> GetAll() // o meu repositorio tem que acessar uma lista de categorias
        {
            return await _context.Categories.ToListAsync();//tornando todos os objetos categorias na memória, em produção isso não é recomendado
        }
        public async Task<IEnumerable<Category>> GetCategoriesProducts() 
        {
            return await _context.Categories.Include(c=> c.Products).ToListAsync();//retornando as categorias e incluindo os produtos
        }
        public async Task<Category> GetById(int id)
        {
           //abaixo eu quero filtrar as categorias cujo Id é igual ao Id que estou recenedo
            return await _context.Categories.Where(c=> c.CategoryId == id).FirstOrDefaultAsync(); //FirstOrDefault ele localiza o primiero item que atende a esse critério
        }
        public async Task<Category> Create(Category category)
        {
            _context.Categories.Add(category); //Vou incluir o objeto que estou recebendo noo contexto, na memória.
            await _context.SaveChangesAsync(); //O repositório pode ou não ter um método para salvar, caso seja rigoroso não pode.O correto seria implemntar o padrão Unit of work e esse SaveChanges dar o commit no Unit of Work
            return category;
        }
        public async Task<Category> Update(Category category)
        {
            _context.Entry(category).State = EntityState.Modified; //O estamos indicando que o estado da entidade é modificado e com isso o Entity...vai entender que teve uma alteraçao nesse objeto e ele vai salvar esse objeto
            await _context.SaveChangesAsync();
            return category;
        }
        public async Task<Category> Delete(int id)
        {
            var category = await GetById(id);
            _context.Categories.Remove(category); //remover esse objeto do contexto
            await _context.SaveChangesAsync();
            return category;
        }


    }
}
