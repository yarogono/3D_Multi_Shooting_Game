using System.ComponentModel.DataAnnotations;

public class GoogleLoginResDto
{
    [StringLength(50)]
    public string email { get; set; }

    [StringLength(20)]
    public string name { get; set; }
}
