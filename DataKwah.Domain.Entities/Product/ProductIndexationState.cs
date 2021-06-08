namespace DataKwah.Domain.Entities
{
    public enum ProductIndexationState : byte
    {
        Requested = 0,
        Pending = 1,
        Done = 2,
        Failed = 3
    }
}
