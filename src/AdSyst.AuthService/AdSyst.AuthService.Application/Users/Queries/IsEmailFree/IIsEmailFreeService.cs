namespace AdSyst.AuthService.Application.Users.Queries.IsEmailFree
{
    public interface IIsEmailFreeService
    {
        Task<bool> CheckAsync(string email, CancellationToken cancellationToken = default);
    }
}
