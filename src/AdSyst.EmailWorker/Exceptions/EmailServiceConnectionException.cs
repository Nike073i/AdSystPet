namespace AdSyst.EmailWorker.Exceptions
{
    public class EmailServiceConnectionException : Exception
    {
        public EmailServiceConnectionException(string message)
            : base(
                $"Возникли проблемы при попытке установления соединения с почтовым сервисом: {message}"
            ) { }
    }
}
