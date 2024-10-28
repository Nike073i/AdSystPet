namespace AdSyst.Advertisments.Infrastructure.Data.Models
{
    public class CategoryReadModel
    {
        public required Guid Id { get; set; }
        public required string Name { get; set; }
        public Guid? ParentCategoryId { get; set; }
        public CategoryReadModel? ParentCategory { get; set; }
        public List<CategoryReadModel>? Children { get; set; }
    }
}
