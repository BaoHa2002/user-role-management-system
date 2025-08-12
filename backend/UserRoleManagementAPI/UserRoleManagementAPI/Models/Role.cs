namespace UserRoleManagementAPI.Models
{
    public class Role
    {
        public int Id { get; set; }
        public string RoleName { get; set; } = null;
        public string? Decription { get; set; }
        public ICollection<User> Users { get; set; } = new List<User>();
    }
}
