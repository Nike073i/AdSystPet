namespace AdSyst.Common.Contracts.EmailMessages
{
    public class EmailMessageSentEvent
    {
        public IEnumerable<string> AddressesTo { get; set; } = null!;
        public string Subject { get; set; } = null!;
        public string Text { get; set; } = null!;
        public bool IsHtml { get; set; }

        public EmailMessageSentEvent() { }

        public EmailMessageSentEvent(
            IEnumerable<string> addressTo,
            string subject,
            string text,
            bool isHtml
        )
        {
            AddressesTo = addressTo;
            Subject = subject;
            Text = text;
            IsHtml = isHtml;
        }

        public EmailMessageSentEvent(string addressTo, string subject, string text, bool isHtml)
            : this(new[] { addressTo }, subject, text, isHtml) { }
    }
}
