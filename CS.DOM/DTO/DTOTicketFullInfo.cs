﻿namespace CS.DOM.DTO
{
    public class DTOTicketFullInfo
    {
        public Guid Id { get; set; }
        public Guid CustomerId { get; set; }
        public string RequestType { get; set; }
        public bool IsAssigned { get; set; }
        public string Topic { get; set; }
        public string Description { get; set; }
        public List<string> AttachmentsFilePath { get; set; }
    }
}