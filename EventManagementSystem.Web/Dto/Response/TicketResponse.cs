namespace EventManagementSystem.Web.Dto.Response
{
    public class EventOverride
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
    public class TicketResponse
    {
        public int Id { get; set; }
        public decimal Price { get; set; }
        public string TicketType { get; set; }
        public EventOverride Event { get; set; }
    }
}
