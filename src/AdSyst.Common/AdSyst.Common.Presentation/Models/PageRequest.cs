using System.ComponentModel.DataAnnotations;

namespace AdSyst.Common.Presentation.Models
{
    public record PageRequest
    {
        /// <summary>
        /// Размер страницы
        /// </summary>
        [Range(1, 250)]
        public int? PageSize { get; init; }

        /// <summary>
        /// Номер страницы
        /// </summary>
        [Range(1, 10000)]
        public int? PageNumber { get; init; }
    }
}
