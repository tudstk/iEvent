using System.Collections.Generic;

//Singleton
namespace AuthenticationService
{
    public class SessionManager
    {
        private static SessionManager _instance;

        private Dictionary<string, string> _userSessions;

        private SessionManager()
        {
            _userSessions = new Dictionary<string, string>();
        }

        public static SessionManager Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new SessionManager();
                }
                return _instance;
            }
        }

        public string CreateSession(User user)
        {
            throw new NotImplementedException();
        }

        public void InvalidateSession(string token)
        {
            throw new NotImplementedException();
        }

        public bool IsSessionValid(string token)
        {
            throw new NotImplementedException();
        }
    }
}
