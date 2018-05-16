using MySql.Data.MySqlClient;
using System.Data;

namespace Zegeltjes_DAL
{
    public class LoginCommand : Command<Zegeltjes_Models.LoginModel>
    {
        private string mGebruikersnaam;
        private string mWachtwoord;
        public LoginCommand(string gebruikersNaam, string wachtwoord)
        {
            mGebruikersnaam = gebruikersNaam;
            mWachtwoord = wachtwoord;
        }
        
        public override Zegeltjes_Models.LoginModel Execute()
        {
            DraaiQuery($"SELECT `GebruikerID`, `Voornaam`, `Achternaam` FROM `gebruiker` WHERE `E-Mail` = '{mGebruikersnaam}' && `Wachtwoord` = '{mWachtwoord}'");
            
            if (dtResult.Rows.Count != 0)
            {
                return new Zegeltjes_Models.LoginModel() {
                    GebruikerID = dtResult.Rows[0][0].ToString(),
                    Voornaam = dtResult.Rows[0][1].ToString(),
                    Achternaam = dtResult.Rows[0][1].ToString()
                };
            }
            else
            {
                return null;
            }
        }
    }
}
