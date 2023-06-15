namespace EventManagementSystem.Web.Entities
{
    public class Sponsor
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int EventId { get; set; }
        public string SponsorshipLevel { get; set; }

        public virtual Event Event { get; set; }
    }
}
