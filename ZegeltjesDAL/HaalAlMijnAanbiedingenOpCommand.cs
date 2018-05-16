using System;
using System.Collections.Generic;
using System.Data;

namespace Zegeltjes_DAL
{
    public class HaalAlMijnAanbiedingenOpCommand : Command<List<Zegeltjes_Models.Aanbieding>>
    {
        private int GebruikerID;
        public HaalAlMijnAanbiedingenOpCommand(int gebruikerID)
        {
            GebruikerID = gebruikerID;
        }
        public override List<Zegeltjes_Models.Aanbieding> Execute()
        {
            DraaiQuery($"SELECT aanbieding.`AanbiedingID`, actie.Naam, aanbieding.Ruilvoorwaarden, aanbieding.Omschrijving, winkelketen.Naam, aanbieding.ActieID FROM aanbieding INNER JOIN actie ON actie.ActieID = aanbieding.ActieID INNER JOIN winkelketen on actie.WinkelID = winkelketen.WinkelID where aanbieding.GebruikerID = '{GebruikerID}'");
            List<Zegeltjes_Models.Aanbieding> Aanbiedingen = new List<Zegeltjes_Models.Aanbieding>();
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
