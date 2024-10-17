using DataGridView.Contracts.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataGridView
{
    internal class DataGenerator
    {
        /// <summary>
        /// Создание нового экземпляр <see cref="Product"/>
        /// </summary>
        public static Product CreateDefaultProduct(Action<Product> addInfo = null)
        {
            var newNail = new Product
            {
                Id = Guid.NewGuid(),
                Name = "",
                Size = 1.0M,
                Material = Materials.Copper.ToString(),
                Quantity = 1,
                MinimumQuantity = 1,
                Price = 0
            };

            addInfo?.Invoke(newNail);

            return newNail;
        }
    }
}
