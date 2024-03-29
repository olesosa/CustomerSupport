﻿using System.ComponentModel.DataAnnotations;

namespace CS.DOM.DTO
{
    public class TicketCreateDto
    {
        public string RequestType { get; set; }
        public string Topic { get; set; }
        public string Description { get; set; }
        public DateTime CreationTime { get; set; }
    }
}
