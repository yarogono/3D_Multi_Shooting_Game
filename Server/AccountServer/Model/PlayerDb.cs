using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace AccountServer.Model
{
    [Table("Player")]
    public class PlayerDb
    {
        [Key]
        public int PlayerId { get; set; }

        public string Nickname { get; set; }

        public DateTime CreatedAt { get; set; }
    }
}
