namespace CustomerSupportData.Models
{
    public abstract class BaseDetails : BaseEntity
    {
        public DateTime? WhenCreated { get; set; }
    }
}
