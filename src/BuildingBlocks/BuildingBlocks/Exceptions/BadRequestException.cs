namespace BuildingBlocks.Exceptions
{
    public class BadRequestException : Exception
    {
        public string? Details { get; }

        public BadRequestException(string mess) : base(mess)
        {

        }

        public BadRequestException(string mess, string details) : base(mess)
        {
            Details = details;
        }
    }
}
