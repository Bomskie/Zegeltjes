using System.Collections.Generic;
using System.Data;

namespace Zegeltjes.Models.Aanbiedingen
{
    public class VoegAanbiedingToeModel
    {
        public List<Zegeltjes_Models.Actie> Acties { get; set; }

        public string actie { get; set; }

        public string type { get; set; }

        public string omschrijving { get; set; }
    }
}
