namespace AdSyst.AuthService.Application.Users.Queries.GetPersonalData
{
    public interface IGetPersonalDataService
    {
        Task<UserPersonalData?> GetAsync(
            string userId,
            CancellationToken cancellationToken = default
        );
    }
}
