namespace AdSyst.Advertisments.Application.Categories.Queries.GetCategoryById
{
    /// <summary>
    /// Модель категории
    /// </summary>
    /// <param name="Id">Идентификатор</param>
    /// <param name="Name">Название</param>
    /// <param name="Children">Дочерние категории</param>
    public record CategoryViewModel(Guid Id, string Name, IEnumerable<CategoryViewModel> Children)
    {
        public CategoryViewModel(Guid id, string name)
            : this(id, name, Enumerable.Empty<CategoryViewModel>()) { }
    }
}
