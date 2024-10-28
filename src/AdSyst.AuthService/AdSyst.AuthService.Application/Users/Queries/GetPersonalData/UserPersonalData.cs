namespace AdSyst.AuthService.Application.Users.Queries.GetPersonalData
{
    public record UserPersonalData(
        string PersonId,
        string FirstName,
        string LastName,
        string Username,
        string Email,
        DateTimeOffset Birthday
    );
}
