using System;

namespace AuthenticationService
{
    public class PasswordRecovery
    {
        public void SendPasswordResetLink(string email)
        {
            throw new NotImplementedException();
        }

        public bool ValidatePasswordResetToken(string token)
        {
            throw new NotImplementedException();
        }

        public bool ResetPassword(string token, string newPassword)
        {
            throw new NotImplementedException();
        }
    }
}
