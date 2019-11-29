using System.Collections.Generic;

namespace Interfaces.Logic
{
    /// <summary>
    /// Defines base functionality for a logic class.
    /// </summary>
    /// <typeparam name="T">Represents model correlating to logic class type</typeparam>
    public interface ILogic<T>
    {
        List<T> GetAll();
        T GetByid(int id);
        void Insert(T obj);
        void Delete(int id);
        void Update(T obj);
    }
}