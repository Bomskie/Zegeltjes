using System;
using System.Collections.Generic;
using System.Data;

namespace Zegeltjes_DAL
{
    public class SelecteerAlleGeldigeActieCommand : Command<List<Zegeltjes_Models.Actie>>
    {
        public override List<Zegeltjes_Models.Actie> Execute()
        {
            string jaar = DateTime.Now.Year.ToString();
            string maand = DateTime.Now.Month.ToString();
            string dag = DateTime.Now.Day.ToString();
            string vandaag = $"{jaar}-{maand}-{dag}";
            DraaiQuery($"SELECT actie.Naam, winkelketen.Naam, actie.ActieID FROM `actie` INNER JOIN `winkelketen` on actie.WinkelID = winkelketen.WinkelID WHERE actie.ActieBegin<='{vandaag}' and actie.ActieEind>='{vandaag}'");
            //return dtResult;
            List<Zegeltjes_Models.Actie> AlleActies = new List<Zegeltjes_Models.Actie>();
            foreach (DataRow dr in dtResult.Rows)
            {
                AlleActies.Add(new Zegeltjes_Models.Actie()
                {
                    ActieID = Convert.ToInt32(dr[2].ToString()),
                    ActieNaam = dr[0].ToString(),
                    WinkelNaam = dr[1].ToString()
                });
            }
            return AlleActies;
        }
    }
}
