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
        /// Отображает список всех продуктов, доступных в системе.
        /// Загружает данные о продуктах и статистику для их отображения на главной странице.
        /// </summary>
        ///<returns>Представление с данными о продуктах.</returns>
        public async Task<IActionResult> Index()
        {
            var products = productManager.GetAllAsync();
            var stats = productManager.GetStatsAsync();
            await Task.WhenAll(products, stats);

            ViewData[nameof(IProductStats)] = stats.Result;
            return View(products.Result);
        }

        /// <summary>
        /// Отображает страницу создания нового продукта.
        /// Предоставляет пользователю форму для ввода данных нового продукта.
        /// </summary>
        /// <returns>Представление для создания продукта.</returns>
        public IActionResult Create()
        {
            return View();
        }

        /// <summary>
        /// Обрабатывает отправку формы для создания нового продукта.
        /// Проверяет корректность данных, задаёт новый уникальный идентификатор
        /// и добавляет продукт в хранилище. При успешной операции перенаправляет 
        /// пользователя на список продуктов.
        /// </summary>
        /// <param name="product">Данные нового продукта, предоставленные пользователем.</param>
        /// <returns>Перенаправление на действие <see cref="Index"/> или повторное отображение формы.</returns>
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
        /// Отображает страницу редактирования существующего продукта.
        /// Выполняет поиск продукта по идентификатору и передаёт его данные в представление
        /// для редактирования.
        /// </summary>
        /// <param name="id">Уникальный идентификатор продукта.</param>
        /// <returns>Представление с формой редактирования или код ошибки 404, если продукт не найден.</returns>
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
        /// Обрабатывает отправку формы редактирования продукта.
        /// Обновляет данные существующего продукта в хранилище, если проверка прошла успешно.
        /// При отсутствии продукта возвращает ошибку 404.
        /// </summary>
        /// <param name="id">Уникальный идентификатор редактируемого продукта.</param>
        /// <param name="product">Обновлённые данные продукта.</param>
        /// <returns>Перенаправление на действие <see cref="Index"/> или повторное отображение формы.</returns>
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
        /// Удаляет продукт из хранилища на основе переданного идентификатора.
        /// При успешном удалении перенаправляет пользователя на список продуктов.
        /// Если продукт с указанным идентификатором не найден, возвращает ошибку 404.
        /// </summary>
        /// <param name="id">Уникальный идентификатор удаляемого продукта.</param>
        /// <returns>Перенаправление на действие <see cref="Index"/> или код ошибки 404.</returns>
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
        /// Отображает страницу политики конфиденциальности.
        /// Используется для предоставления информации пользователям о правилах сбора и обработки их данных.
        /// </summary>
        /// <returns>Представление страницы политики конфиденциальности.</returns>
        public IActionResult Privacy()
        {
            return View();
        }
    }
}
