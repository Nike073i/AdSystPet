namespace AdSyst.AuthService.Api.Configs.IdentityServer.Initialization
{
    public class DataInitializationException : Exception
    {
        public DataInitializationException(string description)
            : base($"Ошибка инициализации данных. Описание - {description}") { }
    }
}
