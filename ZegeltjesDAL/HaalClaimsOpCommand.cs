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
            DraaiQuery($"SELECT gebruiker.Voornaam, gebruiker.Achternaam, gebruiker.GebruikerID, claims.ClaimID FROM claims INNER JOIN gebruiker on gebruiker.GebruikerID = claims.GebruikerID  WHERE claims.AanbiedingID = '{AanbiedingID}'");
            List<Claim> AlleClaims = new List<Claim>();
            foreach (DataRow dr in dtResult.Rows)
            {
                AlleClaims.Add(new Claim() {
                    gebruiker = new Gebruiker()
                    {
                        Voornaam = dr[0].ToString(),
                        Achternaam = dr[1].ToString(),
                        ID = Convert.ToInt32(dr[2].ToString())
                    },
                    ClaimID = Convert.ToInt32(dr[3].ToString())
                });
            }
            return AlleClaims;
        }
    }
}
