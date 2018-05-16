namespace Zegeltjes_DAL
{
    public class VoegAanbiedingToeCommand : Command<bool>
    {
        private int GebruikerID;
        private string Omschrijving;
        private int ActieID;
        private string Type;
        public VoegAanbiedingToeCommand(int gebruikerID, string omschrijving, int actieID, string type)
        {
            GebruikerID = gebruikerID;
            Omschrijving = omschrijving;
            ActieID = actieID;
            Type = type;
        }
        public override bool Execute()
        {
            try
            {
                DraaiQuery($"INSERT INTO `aanbieding`(`GebruikerID`, `ActieID`, `Omschrijving`, `Ruilvoorwaarden`) VALUES ('{GebruikerID}','{ActieID}','{Omschrijving}','{Type}')");
                return true;
            }
            catch (System.Exception)
            {

                return false;
            }
        }
    }
}
