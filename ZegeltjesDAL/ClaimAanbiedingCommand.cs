namespace Zegeltjes_DAL
{
    public class ClaimAanbiedingCommand : Command<bool>
    {
        private int AanbiedingID;
        private int GebruikerID;

        public ClaimAanbiedingCommand(int aanbiedingID, int gebruikerID)
        {
            AanbiedingID = aanbiedingID;
            GebruikerID = gebruikerID;
        }
        public override bool Execute()
        {
            DraaiQuery($"INSERT INTO `claims`(`GebruikerID`, `AanbiedingID`) VALUES ('{GebruikerID}', '{AanbiedingID}')");
            return true;
        }
    }
}
