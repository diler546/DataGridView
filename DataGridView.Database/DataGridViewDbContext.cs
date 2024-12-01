using System.Data.Entity;
using DataGridView.Contracts.Models;

namespace DataGridView.Database
{
    public class DataGridViewDbContext : DbContext
    {
        /// <summary>
        /// Конструктор контекста базы данных
        /// </summary>
        public DataGridViewDbContext() : base("Server=DESKTOP-1DID0GG;Database=DataGridView;Trusted_Connection=True;")
        {
        }

        /// <summary>
        /// Таблица <see cref="Contracts.Models.Product"/> в базе данных
        /// </summary>
        public DbSet<Product> Products { get; set; }
    }
}
