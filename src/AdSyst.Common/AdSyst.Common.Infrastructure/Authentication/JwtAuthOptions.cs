namespace AdSyst.Common.Presentation.Options
{
    public class JwtAuthOptions
    {
        public string? Authority { get; set; }
        public string? Audience { get; set; }
        public bool RequireHttpsMedatada { get; set; }
    }
}
