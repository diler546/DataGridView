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
        /// ���������� ������ ���� ���������, ��������� � �������.
        /// ��������� ������ � ��������� � ���������� ��� �� ����������� �� ������� ��������.
        /// </summary>
        ///<returns>������������� � ������� � ���������.</returns>
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
        /// ������������� ������������ ����� ��� ����� ������ ������ ��������.
        /// </summary>
        /// <returns>������������� ��� �������� ��������.</returns>
        public IActionResult Create()
        {
            return View();
        }

        /// <summary>
        /// ������������ �������� ����� ��� �������� ������ ��������.
        /// ��������� ������������ ������, ����� ����� ���������� �������������
        /// � ��������� ������� � ���������. ��� �������� �������� �������������� 
        /// ������������ �� ������ ���������.
        /// </summary>
        /// <param name="product">������ ������ ��������, ��������������� �������������.</param>
        /// <returns>��������������� �� �������� <see cref="Index"/> ��� ��������� ����������� �����.</returns>
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
        /// ���������� �������� �������������� ������������� ��������.
        /// ��������� ����� �������� �� �������������� � ������� ��� ������ � �������������
        /// ��� ��������������.
        /// </summary>
        /// <param name="id">���������� ������������� ��������.</param>
        /// <returns>������������� � ������ �������������� ��� ��� ������ 404, ���� ������� �� ������.</returns>
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
        /// ������������ �������� ����� �������������� ��������.
        /// ��������� ������ ������������� �������� � ���������, ���� �������� ������ �������.
        /// ��� ���������� �������� ���������� ������ 404.
        /// </summary>
        /// <param name="id">���������� ������������� �������������� ��������.</param>
        /// <param name="product">���������� ������ ��������.</param>
        /// <returns>��������������� �� �������� <see cref="Index"/> ��� ��������� ����������� �����.</returns>
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
        /// ������� ������� �� ��������� �� ������ ����������� ��������������.
        /// ��� �������� �������� �������������� ������������ �� ������ ���������.
        /// ���� ������� � ��������� ��������������� �� ������, ���������� ������ 404.
        /// </summary>
        /// <param name="id">���������� ������������� ���������� ��������.</param>
        /// <returns>��������������� �� �������� <see cref="Index"/> ��� ��� ������ 404.</returns>
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
        /// ���������� �������� �������� ������������������.
        /// ������������ ��� �������������� ���������� ������������� � �������� ����� � ��������� �� ������.
        /// </summary>
        /// <returns>������������� �������� �������� ������������������.</returns>
        public IActionResult Privacy()
        {
            return View();
        }
    }
}
