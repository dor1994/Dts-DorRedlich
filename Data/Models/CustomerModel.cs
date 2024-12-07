
namespace Data.Models
{
    public class CustomerModel
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public string? CustomerName { get; set; }
        public DateTime RequestedTime { get; set; }
        public DateTime? CreatedAt { get; set; }
    }
}
