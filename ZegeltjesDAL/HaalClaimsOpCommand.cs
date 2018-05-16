using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using Zegeltjes_Models;

namespace Zegeltjes_DAL
{
    public class HaalClaimsOpCommand : Command<List<Claim>>
    {
        int AanbiedingID;
        public HaalClaimsOpCommand(int aanbiedingID)
        {
            AanbiedingID = aanbiedingID;
        }
        public override List<Claim> Execute()
        {
            DraaiQuery($"SELECT `ClaimID`, `GebruikerID`, `AanbiedingID` FROM claims WHERE AanbiedingID = '{AanbiedingID}'");
            List<Claim> AlleClaims = new List<Claim>();
            foreach (DataRow dr in dtResult.Rows)
            {
                AlleClaims.Add(new Claim(){
                    ClaimID = Convert.ToInt32(dr[0].ToString()),
                    GebruikerID = Convert.ToInt32(dr[1].ToString()),
                    AanbiedingID = Convert.ToInt32(dr[2].ToString())
                });
            }
            return AlleClaims;
        }
    }
}
