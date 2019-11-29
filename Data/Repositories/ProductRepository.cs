using Interfaces.Contexts;
using Interfaces.Repositories;
using Models.DataModels;
using System.Collections.Generic;

namespace Data.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly IProductContext Context;

        public ProductRepository(IProductContext context)
        {
            Context = context;
        }

        public void Delete(int id)
        {
            Context.Delete(id);
        }

        public List<Product> GetAll()
        {
            return Context.GetAll();
        }

        public Product GetById(int id)
        {
            return Context.GetById(id);
        }

        public void Insert(Product product)
        {
            Context.Insert(product);
        }

        public void Update(Product product)
        {
            Context.Update(product);
        }
    }
}