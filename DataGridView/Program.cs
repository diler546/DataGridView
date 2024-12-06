﻿using System;
using System.Windows.Forms;
using DataGridView.ProductManager;
using DataGridView.Storage.Memory;
using Microsoft.Extensions.Logging;
using Serilog.Extensions.Logging;
using Serilog;
using DataGridView.Database;
using DataGridView.Contracts;

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

            var serilogLogger = new LoggerConfiguration()
                .MinimumLevel.Verbose()
                .WriteTo.Seq("http://localhost:5341", apiKey: "y7yJHv4SMo1xIrNwP68O")
                .CreateLogger();

            var logger = new SerilogLoggerFactory(serilogLogger)
                .CreateLogger<IProductManager>();

            var storage = new ProductStorage();
            var manager = new ProductsManager(storage, logger);
            Application.Run(new MainForm(manager));
        }
    }
}
