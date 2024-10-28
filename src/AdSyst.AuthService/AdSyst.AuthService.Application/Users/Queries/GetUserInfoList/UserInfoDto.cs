namespace AdSyst.AuthService.Application.Users.Queries.GetUserInfoList
{
    public record UserInfoDto(
        string Id,
        string Email,
        string FirstName,
        string LastName,
        bool IsActive
    );
}
