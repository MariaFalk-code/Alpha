using System.ComponentModel.DataAnnotations;

namespace WebApp.Models.Domain;
public class ClientEntity
{
    [Key]
    public int Id { get; set; }
    [Required]
    [StringLength(100, ErrorMessage = "Client name cannot be longer than 100 characters.")]
    public string Name { get; set; } = null!;
}
