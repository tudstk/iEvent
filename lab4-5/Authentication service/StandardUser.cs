namespace AuthenticationService
{

    public class StandardUser : User
    {

        public StandardUser(string userId)
            : base(userId) { }

        public override string GetRole()
        {
            return "StandardUser";
        }

    }
}
