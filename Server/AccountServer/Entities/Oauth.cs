using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static AccountServer.Utils.Define;

namespace AccountServer.Entities
{
    [Table("Oauth")]
    public class Oauth
    {
        [Key]
        public int OauthId { get; set; }

        [Required]
        public string OauthToken { get; set; }

        [Required]
        public OauthType OauthType { get; set; }

        [ForeignKey("AccountId")]
        public Account? Account { get; set; }
    }
}
