namespace AuthenticationService
{
    public abstract class User
    {
        public string UserId { get; protected set; }

        protected User(string userId, string email, string passwordHash)
        {
            UserId = userId;
        }

        public abstract string GetRole();
    }
}
