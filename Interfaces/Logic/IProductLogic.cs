

using Models.DataModels;

namespace Interfaces.Logic
{
    /// <summary>
    /// Defines functionality for a user logic class.
    /// </summary>
    public interface IProductLogic
    {
        Product GetProductByid(int id);
    }
}