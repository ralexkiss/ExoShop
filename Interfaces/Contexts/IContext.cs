using System.Collections.Generic;

namespace Interfaces.Contexts
{
    /// <summary>
    /// Defines base functionality for a context.
    /// </summary>
    /// <typeparam name="T">Represents model correlating to context type.</typeparam>
    public interface IContext<T>
    {
        List<T> GetAll();
        T GetById(int id);
        void Insert(T obj);
        void Delete(int id);
        void Update(T obj);
    }
}