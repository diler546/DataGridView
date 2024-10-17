using DataGridView.Contracts.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DataGridView.Contracts
{
    /// <summary>
    /// Изменение хранилища продуктов
    /// </summary>
    public interface IProductStorage
    {
        /// <summary>
        /// Асинхронное получение всех данных
        /// </summary>
        Task<IReadOnlyCollection<Product>> GetAllAsync();

        /// <summary>
        /// Асинхронное добавление данных
        /// </summary>
        Task<Product> AddAsync(Product product);

        /// <summary>
        /// Асинхронное изменение данных
        /// </summary>
        Task EditAsync(Product product);

        /// <summary>
        /// Асинхронное удаление данных
        /// </summary>
        Task<bool> DeleteAsync(Guid id);
    }
}
