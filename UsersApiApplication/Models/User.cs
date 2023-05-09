namespace UsersApiApplication.Models
{
    public class User
    {
        public Guid ID { get;  set; }
        public string Login { get;  set; } = null!;
        public string Password { get;  set; } = null!;
        public DateTime CreatedDate { get;  set; }

        public Guid UserGroupID { get; set; }
        public UserGroup UserGroup { get;  set; } = null!;

        public Guid UserStateID { get; set; }
        public UserState UserState { get;  set; } = null!;

    }
}
