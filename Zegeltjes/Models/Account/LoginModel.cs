using System.ComponentModel.DataAnnotations;

namespace Zegeltjes.Models.Account
{
    public class LoginModel
    {
        [Required]
        [EmailAddress]
        public string Mail { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Wachtwoord { get; set; }
    }
}
