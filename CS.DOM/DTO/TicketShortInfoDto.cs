﻿namespace CS.DOM.DTO
{
    public class TicketShortInfoDto
    {
        public Guid Id { get; set; }
        public Guid CustomerId { get; set; }
        public string RequestType { get; set; }
        public bool IsAssigned { get; set; }
        public string Topic { get; set; }
    }
}