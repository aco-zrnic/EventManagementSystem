namespace EventManagementSystem.Web.Entities
{
    public class Participant
    {
        public Participant()
        {
            Ticket = new HashSet<Ticket>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string? Email { get; set; }
        public string ContactNumber { get; set; }

        public virtual ICollection<Ticket> Ticket { get; set; }
    }
}
