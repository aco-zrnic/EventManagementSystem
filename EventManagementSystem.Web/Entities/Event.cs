namespace EventManagementSystem.Web.Entities
{
    public class Event
    {
        public Event()
        {
            Sponsor = new HashSet<Sponsor>();
            Staff = new HashSet<Staff>();
            Ticket = new HashSet<Ticket>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public DateTimeOffset Date { get; set; }
        public string Venue { get; set; }

        public virtual ICollection<Sponsor> Sponsor { get; set; }
        public virtual ICollection<Staff> Staff { get; set; }
        public virtual ICollection<Ticket> Ticket { get; set; }
    }
}
