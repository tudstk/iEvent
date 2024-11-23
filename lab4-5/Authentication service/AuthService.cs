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

        [AuthenticationAspect]
        public bool AuthenticateWithEmailAndPassword(string email, string password)
        {
            throw new NotImplementedException();
        }
        
        [AuthenticationAspect]
        public bool AuthenticateWithOAuth(string provider, string accessToken)
        {
            throw new NotImplementedException();
        }
        [AuthenticationAspect]
        public void Logout(User user)
        {
            throw new NotImplementedException();
        }
    }
}
