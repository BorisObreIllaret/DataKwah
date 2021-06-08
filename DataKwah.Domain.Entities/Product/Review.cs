using System;

namespace DataKwah.Domain.Entities
{
    public class Review
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public string Asin { get; set; }
        public string Title { get; set; }
        public string Body { get; set; }
        public byte? Rating { get; set; }
        public DateTime? Date { get; set; }

        public virtual Product Product { get; set; }
    }
}
