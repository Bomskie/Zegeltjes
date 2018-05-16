using MySql.Data.MySqlClient;

namespace Zegeltjes_DAL
{
    public class RegisterCommand : Command<string>
    {
        private string mMail;
        private string mWachtwoord;
        private string mVoornaam;
        private string mAchternaam;
        private string mPostcode;
        private string mHuisnummer;
        private string mStraatnaam;

        public RegisterCommand(string mail, string wachtwoord, string voornaam, string achternaam, string postcode, string huisnummer, string straatnaam)
        {
            mMail = mail;
            mWachtwoord = wachtwoord;
            mVoornaam = voornaam;
            mAchternaam = achternaam;
            mPostcode = postcode;
            mHuisnummer = huisnummer;
            mStraatnaam = straatnaam;
        }
        public override string Execute()
        {
            if (KijkVoorUniekeMail())
            {
                DraaiQuery($"INSERT INTO `gebruiker`(`E-Mail`, `Wachtwoord`, `Voornaam`, `Achternaam`, `Postcode`, `Huisnummer`, `StraatNaam`) VALUES ('{mMail}','{mWachtwoord}','{mVoornaam}','{mAchternaam}','{mPostcode}', '{mHuisnummer}', '{mStraatnaam}')");
                return null;
            }
            else
            {
                return "Mail in gebruik";
            }
        }

        private bool KijkVoorUniekeMail()
        {
            DraaiQuery($"SELECT `GebruikerID` FROM `gebruiker` WHERE `E-Mail` = '{mMail}'");
            if (dtResult.Rows.Count == 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
