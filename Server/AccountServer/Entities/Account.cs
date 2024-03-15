using System.ComponentModel.DataAnnotations;

namespace AccountServer.Entities
{
    public class Account
    {
        [Key]
        public int AccountId { get; set; }

        [Required]
        [StringLength(45)]
        public string AccountName { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        public string Nickname { get; set; }

        [Required]
        public DateTime CreatedAt { get; set; }

        public DateTime ExpiredAt { get; set; }

        public DateTime LastLoginAt { get; set; }
    }
}
