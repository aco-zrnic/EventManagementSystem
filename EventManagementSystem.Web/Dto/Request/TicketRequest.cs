﻿namespace EventManagementSystem.Web.Dto.Request
{
    public class TicketRequest
    {
        public int Id { get; set; }
        public int EventId { get; set; }
        public decimal Price { get; set; }
        public string TicketType { get; set; }
    }
}
