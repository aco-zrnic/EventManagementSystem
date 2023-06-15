namespace EventManagementSystem.Web.Entities
{
    public class Registration
    {
        public int Id { get; set; }
        public int TicketId { get; set; }
        public DateTime RegistrationDate { get; set; }

        public virtual Ticket Ticket { get; set; }
    }
}
