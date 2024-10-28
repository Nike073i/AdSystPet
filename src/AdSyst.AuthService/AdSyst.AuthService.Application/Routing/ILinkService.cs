namespace AdSyst.AuthService.Application.Routing
{
    public interface ILinkService
    {
        string CreateConfirmLink(string token, string userId);
    }
}
