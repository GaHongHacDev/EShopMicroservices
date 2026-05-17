namespace BuildingBlocks.Exceptions
{
    public class InternalServerException : Exception
    {
        public string? Details { get; }

        public InternalServerException(string mess) : base(mess)
        {

        }

        public InternalServerException(string mess, string details) : base(mess)
        {
            Details = details;
        }
    }
}
