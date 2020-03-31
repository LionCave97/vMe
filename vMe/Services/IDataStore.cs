using System;
using System.Collections.Generic;
using System.Threading.Tasks;

//Manges Burger Menu
//Do not remove!!
//The App will break!
//I have not yet found where this is being used
namespace vMe.Services
{
    public interface IDataStore<T>
    {
        Task<bool> AddItemAsync(T item);
        Task<bool> UpdateItemAsync(T item);
        Task<bool> DeleteItemAsync(string id);
        Task<T> GetItemAsync(string id);
        Task<IEnumerable<T>> GetItemsAsync(bool forceRefresh = false);
    }
}
