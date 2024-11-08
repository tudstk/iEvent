namespace AuthenticationService
{
    public class EventOrganizer : User
    {
        public EventOrganizer(string userId, string email, string passwordHash)
            : base(userId, email, passwordHash) { }

        public override string GetRole()
        {
            return "EventOrganizer";
        }

        public void CreateEvent()
        {

        }
    }
}
