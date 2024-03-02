using System.ComponentModel.DataAnnotations;

public class AccountSignupReqDto
{
    [MinLength(3)]
    [StringLength(45)]
    public string AccountName { get; set; }

    [MinLength(5)]
    [StringLength(200)]
    public string Password { get; set; }

    [MinLength(5)]
    [StringLength(200)]
    public string ConfirmPassword { get; set; }

    [MinLength(3)]
    [StringLength(20)]
    public string Nickname { get; set; }
}