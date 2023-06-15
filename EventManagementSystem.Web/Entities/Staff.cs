namespace EventManagementSystem.Web.Entities
{
    public class Staff
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Role { get; set; }
        public int EventId { get; set; }

        public virtual Event Event { get; set; }
    }
}
