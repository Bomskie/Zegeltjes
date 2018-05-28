using System.Collections.Generic;

namespace Zegeltjes_Models
{
    public class Aanbieding
    {
        public int AanbiedingID { get; set; }
        public string RuilVoorwaarden { get; set; }
        public string Omschrijving { get; set; }
        public Actie Actie { get; set; }
        public Gebruiker Gebruiker { get; set; }
        public List<Claim> Claims { get; set; }
    }
}
