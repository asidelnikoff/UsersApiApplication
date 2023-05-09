namespace UsersApiApplication.Models
{
    public enum UserGroups { Admin, User };
    public class UserGroup
    {
        public Guid ID { get;  set; }
        public UserGroups Code { get;  set; }
        public string? Description { get;  set; }
    }
}
