﻿namespace EventManagementSystem.Web.Dto.Response
{
    public class EventResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTimeOffset Date { get; set; }
        public string Venue { get; set; }
    }
}
