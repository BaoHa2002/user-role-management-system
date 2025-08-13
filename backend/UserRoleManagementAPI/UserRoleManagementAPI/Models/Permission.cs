namespace UserRoleManagementAPI.Models
{
    public class Permission
    {
        public int Id { get; set; }
        public int RoleId { get; set; }
        public string FunctionName { get; set; } = null!;
        public bool Access { get; set; }
        public bool Create { get; set; }
        public bool Read { get; set; }
        public bool Update { get; set; }
        public bool Delete { get; set; }
        public bool Export { get; set; }
        public Role Role { get; set; } = null!;
    }
}
