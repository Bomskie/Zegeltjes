using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Zegeltjes_Logic
{
    public class GeoLogic
    {
        public string BepaalPlaats(string Plaats)
        {

            Zegeltjes_DAL.BagCommand bag = new Zegeltjes_DAL.BagCommand();
            string plaatsnaam = null;
            if (Plaats.Any(char.IsDigit))
            {
                Plaats = Plaats.Replace(" ", string.Empty);
                plaatsnaam = bag.HaalPlaatsNaamOpPostcode(Plaats);
            }
            else
            {
                plaatsnaam = bag.HaalPlaatsNaamOpText(Plaats);
            }
            return plaatsnaam;
        }
    }
}
