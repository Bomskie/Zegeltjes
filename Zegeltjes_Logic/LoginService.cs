using System;
using System.Data;

namespace Zegeltjes_Logic
{
    public class LoginService
    {
        private string mail;
        private string wachtwoord;

        public LoginService(string mail, string wachtwoord)
        {
            this.mail = mail;
            this.wachtwoord = wachtwoord;            
        }

        public Zegeltjes_Models.LoginModel CheckLogin()
        {
            Zegeltjes_DAL.LoginCommand login = new Zegeltjes_DAL.LoginCommand(mail, wachtwoord);
            return login.Execute();            
        }
    }
}
