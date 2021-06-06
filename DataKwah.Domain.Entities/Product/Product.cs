using System.Collections.Generic;

namespace DataKwah.Domain.Entities
{
    public class Product
    {
        public int Id { get; set; }
        public string Asin { get; set; }
        public string Label { get; set; }

        public virtual ProductState ProductState { get; set; }
        public virtual ICollection<Review> Reviews { get; set; }
    }
}
