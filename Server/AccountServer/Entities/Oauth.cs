using System.ComponentModel.DataAnnotations;
using static AccountServer.Utils.Define;

namespace AccountServer.Entities
{
    public class Oauth
    {
        [Key]
        public int OauthId { get; set; }

        [Required]
        public string OauthToken { get; set; }

        [Required]
        public OauthType OauthType { get; set; }

        public int? AccountId { get; set; }
    }
}
