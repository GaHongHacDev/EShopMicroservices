namespace BuildingBlocks.NewFolder
{
    public class NotFoundException : Exception
    {
        public NotFoundException(string mess) : base(mess)
        {

        }

        public NotFoundException(string entity, object key) : base($"Entity \"{entity}\" ({key}) was not found.")
        {

        }
    }
}
