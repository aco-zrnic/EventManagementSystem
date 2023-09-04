namespace EventManagementSystem.Web.Dto.Response
{
    public class TicketOverride
    {
        public int Id { get; set; }
        public decimal Price { get; set; }
        public string TicketType { get; set; }
    }
    public class RegistrationResponse
    {
        public int Id { get; set; }
        public int TicketId { get; set; }
        public DateTime RegistrationDate { get; set; }
        public TicketOverride Ticket { get; set;}
    }
}
