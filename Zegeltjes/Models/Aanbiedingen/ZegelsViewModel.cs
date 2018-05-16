using System.Collections.Generic;

namespace Zegeltjes.Models.Aanbiedingen
{
    public class ZegelsViewModel
    {
         public List<Zegeltjes_Models.Actie> Acties { get; set; }
         public List<Zegeltjes_Models.Aanbieding> Aanbiedingen { get; set; }
        public string plaats { get; set; }
    }
}
