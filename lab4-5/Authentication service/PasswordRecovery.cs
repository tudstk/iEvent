using System;

namespace AuthenticationService
{
    public class PasswordRecovery
    {   
        [AuthenticationAspect]
        public void SendPasswordResetLink(string email)
        {
            throw new NotImplementedException();
        }

        [AuthenticationAspect]
        public bool ValidatePasswordResetToken(string token)
        {
            throw new NotImplementedException();
        }

        [AuthenticationAspect]
        public bool ResetPassword(string token, string newPassword)
        {
            throw new NotImplementedException();
        }
    }
}
