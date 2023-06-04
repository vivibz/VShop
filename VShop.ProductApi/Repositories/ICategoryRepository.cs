using VShop.ProductApi.Models;

namespace VShop.ProductApi.Repositories;

public interface ICategoryRepository
{ //Vou definir aqui os métodos que meu repositorio vai precisar ter, vamos definir métodos assync
  // Vou precisar obter uma lista de categorias, vou precisar acessar uma categoria por Id, criar uma categoria, atualizar uma categoria e deletar uma categoria
  // Como vai ser assincrona vamos sempre usar a classe Task
  //estamos definindo abaixo para retornar uma lista de categorias. Em um repositorio não se trabalha com DTOs e sim com entidades
    Task<IEnumerable<Category>> GetAll();
    Task<IEnumerable<Category>> GetCategoriesProducts();
    Task<Category> GetById(int id);
    Task<Category> Create(Category category);
    Task<Category> Update(Category category);
    Task<Category> Delete(int id);

}
