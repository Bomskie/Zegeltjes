using System.ComponentModel.DataAnnotations;

namespace Zegeltjes.Models.Account
{
    public class RegisterModel
    {
        [Required]
        [EmailAddress]
        public string Mail { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Wachtwoord { get; set; }
        [Required]
        public string Voornaam { get; set; }
        [Required]
        public string Achternaam { get; set; }
        [Required]
        public string Postcode { get; set; }
        [Required]
        public string Huisnummer { get; set; }
    }
}
