using AdSyst.Common.BusinessLayer.Enums;

namespace AdSyst.AuthService.Application.Users.Queries.GetUserInfoList
{
    /// <summary>
    /// Опции сортировки пользователей
    /// </summary>
    public record UserSortOptions
    {
        /// <summary>
        /// Поле сортировки
        /// </summary>
        /// <value>По умолчанию сортировка по почте</value>
        public SortUserField SortField { get; }

        /// <summary>
        /// Направление сортировки
        /// </summary>
        /// <value>По умолчанию сортировка по возрастанию</value>
        public SortDirection SortDirection { get; }

        public UserSortOptions(SortUserField? sortField = null, SortDirection? sortDirection = null)
        {
            SortField = sortField ?? SortUserField.Email;
            SortDirection = sortDirection ?? SortDirection.Asc;
        }
    }
}
