namespace EventManagementSystem.Web.Entities
{
    public class Ticket
    {
        public int Id { get; set; }
        public int EventId { get; set; }
        public decimal Price { get; set; }
        public string TicketType { get; set; }

        public virtual Event Event { get; set; }
        public virtual Registration Registration { get; set; }
        public virtual Participant Participant { get; set; }
    }
}
