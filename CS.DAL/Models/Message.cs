﻿namespace CS.DAL.Models
{
    public class Message : BaseEntity
    {
        public bool IsRead { get; set; }
        public Guid UserId { get; set; }
        public User User { get; set; }
        public Guid DialogId { get; set; }
        public Dialog Dialog { get; set; }
        public Guid DetailsId {  get; set; }
        public MessageDetails Details {  get; set; }
        public List<MessageAttachment> Attachments { get; set; }
    }
}