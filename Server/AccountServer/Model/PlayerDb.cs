using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace AccountServer.Model
{
    [Table("Player")]
    public class PlayerDb
    {
        [Key]
        public int PlayerId { get; set; }

        [Required]
        public string PlayerAccountName { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        public string Nickname { get; set; }

        [Required]
        public DateTime CreatedAt { get; set; }
    }
}
