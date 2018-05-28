namespace Zegeltjes_DAL
{
    public class ClaimToekennenCommand : Command<bool>
    {
        private int ClaimId;
        public ClaimToekennenCommand(int claimId)
        {
            ClaimId = claimId;
        }
        public override bool Execute()
        {
            DraaiQuery($"UPDATE `claims` SET `Toegekend` = '1' WHERE `claims`.`ClaimID` = '{ClaimId}'");
            return true; 
        }
    }
}
