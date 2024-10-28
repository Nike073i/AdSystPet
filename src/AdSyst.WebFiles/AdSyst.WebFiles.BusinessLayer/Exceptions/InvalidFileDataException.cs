namespace AdSyst.WebFiles.BusinessLayer.Exceptions
{
    public class InvalidFileDataException : Exception
    {
        public InvalidFileDataException(string reason)
            : base($"Файл имеет недопустимые данные - {reason}") { }
    }
}
