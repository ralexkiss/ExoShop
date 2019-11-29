using Data;
using Data.Repositories;
using Interfaces.Contexts;
using Interfaces.Logic;
using Interfaces.Repositories;
using Models.DataModels;
using System.Collections.Generic;

namespace Logic.LogicObjects
{
    public class ProductLogic : IProductLogic
    {
        private readonly IProductRepository productRepository;

        public ProductLogic(IProductContext context)
        {
            productRepository = new ProductRepository(context);
        }

        public void Delete(int id)
        {
            productRepository.Delete(id);
        }

        public List<Product> GetAll()
        {
            return productRepository.GetAll();
        }

        public Product GetByid(int id)
        {
            return productRepository.GetById(id);
        }

        public void Insert(Product obj)
        {
            productRepository.Insert(obj);
        }

        public void Update(Product obj)
        {
            productRepository.Update(obj);
        }
    }
}