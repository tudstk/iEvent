using System;

namespace AuthenticationService
{
    public class AuthService
    {

        private readonly SessionManager _sessionManager;
        
        public AuthService(SessionManager sessionManager)
        {
            _sessionManager = sessionManager;
        }

        public bool AuthenticateWithEmailAndPassword(string email, string password)
        {
            throw new NotImplementedException();
        }

        public bool AuthenticateWithOAuth(string provider, string accessToken)
        {
            throw new NotImplementedException();
        }

        public void Logout(User user)
        {
            throw new NotImplementedException();
        }
    }
}
