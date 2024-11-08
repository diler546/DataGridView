using System;
using Microsoft.Extensions.Logging;

namespace DataGridView.ProductManager
{
    /// <summary>
    /// Вспомогательный класс для логирования информации и ошибок при выполнении операций с продуктами.
    /// </summary>
    static internal class LoggingHelper
    {
        private const string InfoLoggerTemplateAvto =
           "Сделано {0} для товара с идентификатором {1} и именем \"{2}\", прошло время: {3} мс; дата: {4}";
        private const string ErrorLoggerTemplateAvto =
            "Не удалось заполнить {0} для товара с идентификатором {1} и именем \"{2}\", прошло время: {3} мс; дата: {4}; сообщение об ошибке: {5}";
        private const string ErrorLoggerTemplateCommon =
            "Не удалось завершить {0}, дата: {1}; сообщение об ошибке: {2}";

        /// <summary>
        /// Логирует ошибку, произошедшую при выполнении операции с продуктом.
        /// </summary>
        /// <param name="logger">Экземпляр <see cref="ILogger"/> для записи лога.</param>
        /// <param name="actionName">Название действия, которое вызвало ошибку.</param>
        /// <param name="applicantId">Идентификатор продукта.</param>
        /// <param name="msElapsed">Время, затраченное на выполнение операции, в миллисекундах.</param>
        /// <param name="errorMessage">Сообщение с описанием ошибки.</param>
        /// <param name="applicantName">Имя продукта, с которым произошло действие.</param>
        public static void LogErrorProduct(ILogger logger, string actionName, Guid applicantId, long msElapsed, string errorMessage, string applicantName = null)
        {
            logger.LogError(
                string.Format(
                            ErrorLoggerTemplateAvto,
                            actionName,
                            applicantId,
                            applicantName ?? "unknown",
                            msElapsed,
                            DateTime.Now,
                            errorMessage
                            )
                );
        }

        /// <summary>
        /// Логирует информацию об успешном выполнении операции с продуктом.
        /// </summary>
        /// <param name="logger">Экземпляр <see cref="ILogger"/> для записи лога.</param>
        /// <param name="actionName">Название выполненного действия.</param>
        /// <param name="applicantId">Идентификатор продукта.</param>
        /// <param name="msElapsed">Время, затраченное на выполнение операции, в миллисекундах.</param>
        /// <param name="applicantName">Имя продукта, с которым выполнено действие.</param>
        public static void LogInfoProduct(ILogger logger, string actionName, Guid applicantId, long msElapsed, string applicantName = null)
        {
            logger.LogInformation(
                string.Format(
                              InfoLoggerTemplateAvto,
                              actionName,
                              applicantId,
                              applicantName ?? "unknown",
                              msElapsed,
                              DateTime.Now
                              )
                );
        }

        /// <summary>
        /// Логирует общую ошибку, не связанную с конкретным продуктом.
        /// </summary>
        /// <param name="logger">Экземпляр <see cref="ILogger"/> для записи лога.</param>
        /// <param name="actionName">Название действия, вызвавшего ошибку.</param>
        /// <param name="errorMessage">Сообщение с описанием ошибки.</param>
        public static void LogError(ILogger logger, string actionName, string errorMessage)
        {
            logger.LogError(string.Format(
                                          ErrorLoggerTemplateCommon,
                                          actionName,
                                          DateTime.Now,
                                          errorMessage
                                          )
                );
        }
    }
}
