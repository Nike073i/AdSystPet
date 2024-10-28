namespace AdSyst.Advertisments.Api.Endpoints.Categories.Editor.Update
{
    public class UpdateCategoryRequest
    {
        public Guid Id { get; set; }
        public required string Name { get; set; }
    }
}
