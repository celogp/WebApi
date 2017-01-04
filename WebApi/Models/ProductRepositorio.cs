using System;
using System.Collections.Generic;
using System.Linq;

namespace WebApi.Models
{
    public class ProductRepositorio : IProductRepositorio
    {
        static List<Product> ProductList = new List<Product>();
        

        public void Add(Product item)
        {
            ProductList.Add(item);
            //throw new NotImplementedException();

        }

        public Product Find(int key)
        {
            return ProductList
                            .Where(e => e.Id.Equals(key))
                            .SingleOrDefault();
            //throw new NotImplementedException();
        }

        public IEnumerable<Product> GetAll()
        {
            return ProductList;
            //throw new NotImplementedException();
        }

        public void Remove(int Id)
        {
            var itemToRemove = ProductList.SingleOrDefault(r => r.Id == Id);
            if (itemToRemove != null)
            {
                ProductList.Remove(itemToRemove);
            }
            //throw new NotImplementedException();
        }

        public void Update(Product item)
        {
            var itemToUpdate = ProductList.SingleOrDefault(r => r.Id == item.Id);
            if (itemToUpdate != null)
            {
                itemToUpdate.Name = item.Name;
                itemToUpdate.Category = item.Category;
                itemToUpdate.Price = item.Price;
            }
            //throw new NotImplementedException();
        }
    }
}