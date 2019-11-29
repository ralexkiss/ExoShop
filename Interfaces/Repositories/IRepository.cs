using System.Collections.Generic;

namespace Interfaces.Repositories
{
    /// <summary>
    /// Defines base functionality for a repository.
    /// </summary>
    /// <typeparam name="T">Represents model correlating to repository type.</typeparam>
    public interface IRepository<T>
    {
        List<T> GetAll();
        T GetById(int id);
        void Insert(T obj);
        void Delete(int id);
        void Update(T obj);
    }
}