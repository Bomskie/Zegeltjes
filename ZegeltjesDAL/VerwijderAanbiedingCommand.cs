namespace Zegeltjes_DAL
{
    public class VerwijderAanbiedingCommand : Command<bool>
    {
        private int AanbiedingID;
        public VerwijderAanbiedingCommand(int aanbiedingID)
        {
            AanbiedingID = aanbiedingID;
        }
        public override bool Execute()
        {
            DraaiQuery($"DELETE FROM `aanbieding` WHERE `AanbiedingID` = '{AanbiedingID}'");
            return true;
        }
    }
}