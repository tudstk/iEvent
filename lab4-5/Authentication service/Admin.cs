namespace AuthenticationService
{
    public class Admin : User
    {
        public Admin(string userId)
            : base(userId) { }

        public override string GetRole()
        {
            return "Admin";
        }

        public void ManageUsers()
        {
            
        }
    }
}
