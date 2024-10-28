namespace AdSyst.Advertisments.Api.Endpoints.Categories.Editor.Create
{
    public class CreateCategoryRequest
    {
        public required string Name { get; set; }
        public Guid? ParentCategoryId { get; set; }
    }
}
