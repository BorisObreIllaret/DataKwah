namespace DataKwah.Core.Filter
{
    public interface IFilterRequest
    {
        int? Page { get; set; }
        int? Limit { get; set; }
        string Sort { get; set; }
        bool AscendingOrder { get; set; }
        string Search { get; set; }
    }
}
