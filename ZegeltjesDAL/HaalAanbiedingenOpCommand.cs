using System;
using System.Collections.Generic;
using System.Data;
using Zegeltjes_Models;

namespace Zegeltjes_DAL
{
    public class HaalAanbiedingenOpCommand : Command<List<Aanbieding>>
    {
        string Plaats;
        public HaalAanbiedingenOpCommand(string plaats)
        {
            Plaats = plaats;
        }
        public override List<Aanbieding> Execute()
        {
            DraaiQuery($"SELECT aanbieding.`AanbiedingID`, actie.Naam, aanbieding.Ruilvoorwaarden, aanbieding.Omschrijving, winkelketen.Naam, gebruiker.GebruikerID, aanbieding.ActieID FROM aanbieding INNER JOIN actie ON actie.ActieID = aanbieding.ActieID INNER JOIN winkelketen on actie.WinkelID = winkelketen.WinkelID INNER JOIN gebruiker ON aanbieding.GebruikerID = gebruiker.GebruikerID where gebruiker.Plaats = '{Plaats}' and aanbieding.AanbiedingID not in(SELECT claims.AanbiedingID from claims WHERE claims.Toegekend = 1)");

            List<Aanbieding> Aanbiedingen = new List<Zegeltjes_Models.Aanbieding>();
            foreach (DataRow dr in dtResult.Rows)
            {
                Aanbiedingen.Add(new Zegeltjes_Models.Aanbieding()
                {
                    AanbiedingID = Convert.ToInt32(dr[0].ToString()),
                    RuilVoorwaarden = dr[2].ToString(),
                    Omschrijving = dr[3].ToString(),
                    Actie = new Zegeltjes_Models.Actie()
                    {
                        ActieNaam = dr[1].ToString(),
                        WinkelNaam = dr[4].ToString(),
                        ActieID = Convert.ToInt32(dr[5].ToString())
                    }
                });
            }
            return Aanbiedingen;
        }
    }
}
