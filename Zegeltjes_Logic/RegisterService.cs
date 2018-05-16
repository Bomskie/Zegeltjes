namespace Zegeltjes_Logic
{
    public class RegisterService
    {
        private string mMail;
        private string mWachtwoord;
        private string mVoornaam;
        private string mAchternaam;
        private string mPostcode;
        private string mHuisnummer;

        private string mStraatNaam;
        Zegeltjes_DAL.BagCommand bag;

        public RegisterService(string mail, string wachtwoord, string voornaam, string achternaam, string postcode, string huisnummer)
        {
            mMail = mail;
            mWachtwoord = wachtwoord;
            mVoornaam = voornaam;
            mAchternaam = achternaam;
            mPostcode = postcode;
            mHuisnummer = huisnummer;

            bag = new Zegeltjes_DAL.BagCommand();
        }

        public string RegristreerGebruiker()
        {
            mStraatNaam = bag.HaalStraatNaamOp(mPostcode, mHuisnummer);
            if (mStraatNaam != "")
            {
                Zegeltjes_DAL.RegisterCommand registerCommand = new Zegeltjes_DAL.RegisterCommand(mMail, mWachtwoord, mVoornaam, mAchternaam, mPostcode, mHuisnummer, mStraatNaam);
                return registerCommand.Execute();                
            }
            else
            {
                return "Er is iets mis gegaan";
            }
        }
    }
}
