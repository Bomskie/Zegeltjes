namespace Zegeltjes_Logic
{
    public class AccountLogic
    {
        public Zegeltjes_Models.LoginModel LogGebruikerIn(string mail, string wachtwoord)
        {
            Zegeltjes_DAL.LoginCommand login = new Zegeltjes_DAL.LoginCommand(mail, wachtwoord);
            return login.Execute();
        }

        public string RegistreerGebruiker(string mail, string wachtwoord, string voornaam, string achternaam, string postcode, string huisnummer)
        {
            Zegeltjes_DAL.BagCommand bag = new Zegeltjes_DAL.BagCommand();
            string straatNaam = bag.HaalStraatNaamOp(postcode, huisnummer);
            if (straatNaam != "")
            {
                Zegeltjes_DAL.RegisterCommand registerCommand = new Zegeltjes_DAL.RegisterCommand(mail, wachtwoord, voornaam, achternaam, postcode, huisnummer, straatNaam);
                return registerCommand.Execute();
            }
            else
            {
                return "Er is iets mis gegaan";
            }
        }
    }
}
