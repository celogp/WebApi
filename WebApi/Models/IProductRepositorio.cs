using System.Collections.Generic;

namespace WebApi.Models
{
    public interface IProductRepositorio
    {
        void Add(Product item);
        IEnumerable<Product> GetAll();
        Product Find(int key);
        void Remove(int Id);
        void Update(Product item);
    }
}
