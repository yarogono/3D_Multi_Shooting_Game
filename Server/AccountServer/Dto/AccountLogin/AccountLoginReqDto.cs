using System.ComponentModel.DataAnnotations;

public class AccountLoginReqDto
{
    [StringLength(45)]
    public string AccountName { get; set; }

    [StringLength(200)]
    public string Password { get; set; }
}