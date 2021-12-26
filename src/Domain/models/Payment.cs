using Domain.Enums;

namespace Domain.Models
{
    public class Payment
    {
        public int Id { get; set; }

        public int AccountId { get; set; }

        public decimal Sum { get; set; }

        public PaymentTypeEnum PaymentType { get; set; }
    }
}
