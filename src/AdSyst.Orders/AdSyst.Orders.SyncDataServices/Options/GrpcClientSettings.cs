namespace AdSyst.Orders.SyncDataServices.Options
{
    public class GrpcClientSettings
    {
        public string Scheme { get; set; } = "http";

        public string Host { get; set; } = null!;

        public int Port { get; set; }

        public Uri Address
        {
            get
            {
                var uriBuilder = new UriBuilder
                {
                    Host = Host,
                    Port = Port,
                    Scheme = Scheme
                };
                return uriBuilder.Uri;
            }
        }
    }
}
