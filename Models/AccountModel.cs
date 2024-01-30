using CustomerApi.Helpers;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CustomerApi.Models;

[Table("Accounts")]
public class AccountModel
{
    [Key]
    public int Id { get; set; }
    [ForeignKey("CustomerId")]
    public int CustomerId { get; set; }
    [Required, MaxLength(12)]
    public string AccountNumber { get; set; }
    [Required]
    public string AccountType { get; set; }
    [Required, MaxLength(50)]
    public string BranchAddress { get; set; }
    public decimal InitialDeposit { get; set; }
}
