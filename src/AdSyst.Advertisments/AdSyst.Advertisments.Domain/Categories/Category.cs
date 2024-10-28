using AdSyst.Advertisments.Domain.Abstractions;

namespace AdSyst.Advertisments.Domain.Categories
{
    /// <summary>
    /// Модель сущности категории
    /// </summary>
    public class Category : Entity<Guid>
    {
        /// <summary>
        /// Идентификатор родительской категории
        /// </summary>
        /// <value>
        /// Данное значение может быть установлено для указания иерархии категорий
        /// </value>
        public Guid? ParentCategoryId { get; set; }

        /// <summary>
        /// Название
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Навигационное свойство родительской категории
        /// </summary>
        /// <value>
        /// Навигационное свойство типа объявления может иметь значение,
        /// если категория является подкатегорией
        /// и была подгружена связанная сущность
        /// </value>
        public Category? ParentCategory { get; set; }

        /// <summary>
        /// Навигационное свойство дочерних категорий
        /// </summary>
        /// <value>
        /// Навигационное свойство дочерних категорий может иметь значение,
        /// когда были подгружены связанные сущности.
        /// При отсутствии связанных сущностей коллекция будет пустая.
        /// </value>
        public ICollection<Category>? Children { get; init; }

        public Category(string name)
        {
            Name = name;
        }
    }
}
