using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace CustomerApi.Models.DTO;

public class AccountDTO
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
}
