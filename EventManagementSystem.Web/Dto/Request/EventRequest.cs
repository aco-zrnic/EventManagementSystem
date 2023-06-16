namespace EventManagementSystem.Web.Dto.Request
{
    public class EventRequest
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTimeOffset Date { get; set; }
        public string Venue { get; set; }
    }
}
