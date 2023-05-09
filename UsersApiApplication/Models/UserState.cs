namespace UsersApiApplication.Models
{
    public enum UserStates { Active, Blocked };
    public class UserState
    {
        public Guid ID { get;  set; }
        public UserStates Code { get;  set; }
        public string? Description { get;  set; }
    }
}
