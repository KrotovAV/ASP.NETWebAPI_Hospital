using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalPresentationLayer.Models.Api.ListViewModels.Options
{
    /// <summary>
    /// Порядок сортировки
    /// </summary>
    public enum OrderDirection
    {
        /// <summary>
        /// Без сортировки
        /// </summary>
        None = 0,

        /// <summary>
        /// По возрастанию
        /// </summary>
        Asc,

        /// <summary>
        /// По убыванию
        /// </summary>
        Desc
    }
}
