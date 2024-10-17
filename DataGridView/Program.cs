using System;
using System.Windows.Forms;
using DataGridView.ProductManager;
using DataGridView.Storage.Memory;

namespace DataGridView
{
    internal static class Program
    {
        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            var storage = new MemoryProductStorage();
            var manager = new ProductsManager(storage);
            Application.Run(new MainForm(manager));
        }
    }
}
