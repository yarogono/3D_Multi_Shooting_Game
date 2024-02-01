using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace AccountServer.Model
{
    [Table("Account")]
    public class Account
    {
        [Key]
        public int AccountId { get; set; }

        [Required]
        public string AccountName { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        public string Nickname { get; set; }

        [Required]
        public DateTime CreatedAt { get; set; }
    }
}
