namespace AdSyst.Advertisments.Api.Endpoints.Categories.Common.GetAll
{
    public class CategoryResponse
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public List<CategoryResponse> Children { get; set; }

        public CategoryResponse(Guid id, string name)
        {
            Id = id;
            Name = name;
            Children = new();
        }
    }
}
