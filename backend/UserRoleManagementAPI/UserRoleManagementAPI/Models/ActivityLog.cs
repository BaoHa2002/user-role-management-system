namespace UserRoleManagementAPI.Models
{
    public class ActivityLog
    {
        public long Id { get; set; }
        public int? UserId { get; set; }
        public string Action { get; set; } = null!;
        public string? Details { get; set; }
        public string? IPAddress { get; set; }
        public string? UserAgent { get; set; }
        public DateTime CreatedAt { get; set; }
        public User? User { get; set; }
    }
}
