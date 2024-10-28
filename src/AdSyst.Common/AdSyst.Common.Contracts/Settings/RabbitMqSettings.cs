namespace AdSyst.Common.Contracts.Settings
{
    public class RabbitMqSettings
    {
        public string ConnectionString { get; set; } = null!;
        public string User { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string ServiceName { get; set; } = null!;
    }
}
