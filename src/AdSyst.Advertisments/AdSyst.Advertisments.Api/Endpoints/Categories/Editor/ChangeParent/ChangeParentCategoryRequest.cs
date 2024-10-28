namespace AdSyst.Advertisments.Api.Endpoints.Categories.Editor.ChangeParent
{
    public class ChangeParentCategoryRequest
    {
        public Guid Id { get; set; }
        public Guid? ParentCategoryId { get; set; }
    }
}
