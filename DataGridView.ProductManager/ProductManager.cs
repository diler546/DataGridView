using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataGridView.Contracts;
using DataGridView.Contracts.Models;
using DataGridView.ProductManager.Models;

namespace DataGridView.ProductManager
{
    /// <inheritdoc cref="IProductManager"/>
    public class ProductsManager : IProductManager
    {
        private IProductStorage productStorage;

        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="ProductsManager"/> с указанным хранилищем продуктов.
        /// </summary>
        /// <param name="productStorage">Объект, реализующий интерфейс <see cref="IProductStorage"/>, используемый для управления продуктами.</param>
        public ProductsManager(IProductStorage productStorage)
        {
            this.productStorage = productStorage;
        }

        /// <inheritdoc cref="IProductManager.AddAsync(Product)"/>
        public async Task<Product> AddAsync(Product product)
        {
            var result = await productStorage.AddAsync(product);
            return result;
        }

        /// <inheritdoc cref="IProductManager.DeleteAsync(Guid)"/>
        public async Task<bool> DeleteAsync(Guid id)
        {
            var result = await productStorage.DeleteAsync(id);
            return result;
        }

        /// <inheritdoc cref="IProductManager.EditAsync(Product)"/>
        public Task EditAsync(Product product)
            => productStorage.EditAsync(product);

        /// <inheritdoc cref="IProductManager.GetAllAsync"/>
        public Task<IReadOnlyCollection<Product>> GetAllAsync()
            => productStorage.GetAllAsync();

        /// <inheritdoc cref="IProductManager.GetStatsAsync"/>
        public async Task<IProductStats> GetStatsAsync()
        {
            var product = await productStorage.GetAllAsync();
            return new ProductStatsModel
            {
                TotalAmount = product.Count,
                FullPriceNoNDS = product.Select(x => x.Quantity * x.Price).Sum(),
                FullPriceWithNDS = product.Select(x => x.Quantity * x.Price).Sum() * 1.2m,
            };
        }
    }
}
