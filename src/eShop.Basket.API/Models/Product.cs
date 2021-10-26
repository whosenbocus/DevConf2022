using System.ComponentModel.DataAnnotations;

public class Product
{
    [Key]
    [Required]
    public int Id { get; set; }

    [Required]
    public int ExternalID { get; set; }

    [Required]
    public string Name { get; set; }

    public ICollection<Purchase> Purchases { get; set; } = new List<Purchase>();
}