using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataKwah.Domain.Entities
{
    public class ProductState
    {
        public int Id { get; set; }
        public byte StateId { get; set; }
        public DateTime? StateDate { get; set; }
        public string Reason { get; set; }

        public virtual Product Product { get; set; }

        [NotMapped]
        public ProductIndexationState State
        {
            get => (ProductIndexationState)StateId;
            set => StateId = (byte)value;
        }
    }
}
