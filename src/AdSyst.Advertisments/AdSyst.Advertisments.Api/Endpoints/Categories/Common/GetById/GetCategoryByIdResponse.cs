namespace AdSyst.Advertisments.Api.Endpoints.Categories.Common.GetById
{
    public record GetCategoryByIdResponse(
        Guid Id,
        string Name,
        IEnumerable<CategoryResponse> Children
    );
}
