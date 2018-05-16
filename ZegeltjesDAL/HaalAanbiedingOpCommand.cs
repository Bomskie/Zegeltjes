using System;
using System.Data;

namespace Zegeltjes_DAL
{
    public class HaalAanbiedingOpCommand : Command<Zegeltjes_Models.Aanbieding>
    {
        private int AanbiedingID;
        public HaalAanbiedingOpCommand(int aanbiedingID)
        {
            AanbiedingID = aanbiedingID;
        }
        public override Zegeltjes_Models.Aanbieding Execute()
        {
            DraaiQuery("SELECT aanbieding.GebruikerID, actie.Naam, winkelketen.Naam, gebruiker.Voornaam, aanbieding.Ruilvoorwaarden, aanbieding.Omschrijving, actie.ActieID FROM aanbieding INNER JOIN actie on aanbieding.ActieID = actie.ActieID"+
                        " INNER JOIN winkelketen on winkelketen.WinkelID = actie.WinkelID"+
                        " INNER JOIN gebruiker on aanbieding.GebruikerID = gebruiker.GebruikerID"+
                        $" WHERE aanbieding.AanbiedingID = '{AanbiedingID}'");

            if (dtResult.Rows.Count != 0)
            {
                return new Zegeltjes_Models.Aanbieding()
                {
                    //GebruikerID = Convert.ToInt32(dtResult.Rows[0][0].ToString()),
                    //ActieNaam = dtResult.Rows[0][1].ToString(),
                    //WinkelNaam = dtResult.Rows[0][2].ToString(),
                    //GebruikerVoornaam = dtResult.Rows[0][3].ToString(),
                    RuilVoorwaarden = dtResult.Rows[0][4].ToString(),
                    Omschrijving = dtResult.Rows[0][5].ToString(),
                    AanbiedingID = AanbiedingID,
                    Actie = new Zegeltjes_Models.Actie()
                    {
                        ActieNaam = dtResult.Rows[0][1].ToString(),
                        WinkelNaam = dtResult.Rows[0][2].ToString(),
                        ActieID = Convert.ToInt32(dtResult.Rows[0][6].ToString())
                    },
                    Gebruiker = new Zegeltjes_Models.Gebruiker()
                    {
                        ID = Convert.ToInt32(dtResult.Rows[0][0].ToString()),
                        Voornaam = dtResult.Rows[0][3].ToString()
                    }
                };
            }
            else
            {
                return null;
            }
        }
    }
}
