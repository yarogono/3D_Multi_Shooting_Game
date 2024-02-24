using System.ComponentModel.DataAnnotations;

public class AccountSignupReqDto
{
    [StringLength(45)]
    public string AccountName { get; set; }

    [StringLength(200)]
    public string Password { get; set; }

    [StringLength(200)]
    public string ConfirmPassword { get; set; }

    [StringLength(20)]
    public string Nickname { get; set; }
}