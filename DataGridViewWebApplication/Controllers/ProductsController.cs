using DataGridView.Contracts.Models;
using DataGridView.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace DataGridViewWebApplication.Controllers
{
    public class ProductsController : Controller
    {
        private readonly IProductManager productManager;

        public ProductsController(IProductManager productManager)
        {
            this.productManager = productManager;
        }

        /// <summary>
        /// ���������� ������ ���� ���������.
        /// </summary>
        public async Task<IActionResult> Index()
        {
            var products = productManager.GetAllAsync();
            var stats = productManager.GetStatsAsync();
            await Task.WhenAll(products, stats);

            ViewData[nameof(IProductStats)] = stats.Result;
            return View(products.Result);
        }

        /// <summary>
        /// ���������� �������� �������� ������ ��������.
        /// </summary>
        public IActionResult Create()
        {
            return View();
        }

        /// <summary>
        /// ������� ����� �������.
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> Create(Product product)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            product.Id = Guid.NewGuid();
            await productManager.AddAsync(product);
            return RedirectToAction(nameof(Index));
        }

        /// <summary>
        /// ���������� �������� �������������� �������� �� ��� ��������������.
        /// </summary>
        public async Task<IActionResult> Edit(Guid id)
        {
            var products = await productManager.GetAllAsync();
            var product = products.FirstOrDefault(p => p.Id == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        /// <summary>
        /// ����������� ������������ �������.
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> Edit(Guid id, Product product)
        {
            if (!ModelState.IsValid)
            {
                return View(product);
            }

            var products = await productManager.GetAllAsync();
            var existingProduct = products.FirstOrDefault(p => p.Id == id);
            if (existingProduct == null)
            {
                return NotFound();
            }

            await productManager.EditAsync(product);
            return RedirectToAction(nameof(Index));
        }

        /// <summary>
        /// ������� ������� �� ��� ��������������.
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> Delete(Guid id)
        {
            var result = await productManager.DeleteAsync(id);
            if (result == false)
            {
                return NotFound();
            }
            return RedirectToAction(nameof(Index));
        }

        /// <summary>
        /// ���������� �������� ������������������.
        /// </summary>
        public IActionResult Privacy()
        {
            return View();
        }
    }
}
