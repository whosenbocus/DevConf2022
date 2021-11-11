using System.ComponentModel.DataAnnotations;

public class ProductCreate
{
    [Required]
    public string Name { get; set; }
    [Required]
    public int Quantity { get; set; }
    [Required]
    public string Supplier { get; set; }
}