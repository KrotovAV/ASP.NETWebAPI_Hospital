
namespace HospitalPresentationLayer.Models.Api.ListViewModels.Options
{
    /// <summary>
    /// Настройки пагинации
    /// </summary>
    public class PageOptions
    {
        /// <summary>
        /// Количество позиций по результату запроса
        /// </summary>
        public int Count { get; set; }

        /// <summary>
        /// Количество позиций на 1 страницы
        /// </summary>
        public int PageSize { get; set; } = 10;

        /// <summary>
        /// Количество страниц
        /// </summary>
        public int PageTotal { get; set; }

        /// <summary>
        /// Номер текущей страницы
        /// </summary>
        public int PageCurrent { get; set; } = 1;
    }
}
