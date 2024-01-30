using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CustomerApi.Models;

[Table("Customers")]
public class CustomerModel
{
    [Key]
    public int Id { get; set; }
    [Required, MaxLength(20)]
    public string FirstName { get; set; }
    [Required, MaxLength(20)]
    public string LastName { get; set; }
    [Required, MaxLength(20)]
    public string MiddleName { get; set; }
    [NotMapped]
    public string FullName { get;set; }
    [Required]
    public DateTime DateOfBirth { get; set; }
    public int Age { get; set; }
    public bool IsFilipino { get; set; }
}
