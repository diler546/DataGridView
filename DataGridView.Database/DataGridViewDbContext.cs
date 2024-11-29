using System.Data.Entity;
using DataGridView.Contracts.Models;

namespace DataGridView.Database
{
    public class DataGridViewDbContext : DbContext
    {
        /// <summary>
        /// Конструктор контекста базы данных
        /// </summary>
        public DataGridViewDbContext() : base("DataGridConnectionString")
        {
        }

        /// <summary>
        /// Таблица <see cref="Product"/> в базе данных
        /// </summary>
        public DbSet<Product> Product { get; set; }
    }
}
