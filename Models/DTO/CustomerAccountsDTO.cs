using System.ComponentModel.DataAnnotations;

namespace CustomerApi.Models.DTO;

public class CustomerAccountsDTO
{
    [Key]
    public int Id { get; set; }
    [Required, MaxLength(20)]
    public string FirstName { get; set; }
    [Required, MaxLength(20)]
    public string LastName { get; set; }
    [Required, MaxLength(20)]
    public string MiddleName { get; set; }
    public string FullName { get; set; }
    [Required]
    public DateTime DateOfBirth { get; set; }
    public int Age { get; set; }
    public bool IsFilipino { get; set; }
    public List<AccountModel> Accounts { get; set; } = new List<AccountModel>();
}
