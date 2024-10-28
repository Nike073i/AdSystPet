namespace AdSyst.EmailWorker.Settings
{
    public class EmailSettings
    {
        public string Host { get; set; } = null!;
        public int TlsPort { get; set; }
        public string Username { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string AddressFrom => Username;
        public string AddressFromName { get; set; } = null!;
    }
}
