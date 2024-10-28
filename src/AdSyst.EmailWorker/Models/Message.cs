namespace AdSyst.EmailWorker.Models
{
    public record Message(IEnumerable<string> AddressesTo, string Subject, string Text, bool IsHtml)
    {
        public Message(string addressTo, string subject, string text, bool isHtml)
            : this(new[] { addressTo }, subject, text, isHtml) { }
    }
}
