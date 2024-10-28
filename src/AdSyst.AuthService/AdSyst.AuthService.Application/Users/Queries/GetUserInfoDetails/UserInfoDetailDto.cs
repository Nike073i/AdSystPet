namespace AdSyst.AuthService.Application.Users.Queries.GetUserInfoDetails
{
    public record UserInfoDetailDto(
        string Id,
        string Email,
        string FirstName,
        string LastName,
        string UserName,
        bool IsActive,
        string[] Roles,
        bool EmailConfirmed,
        DateTimeOffset Birthday
    );
}
