using System.ComponentModel.DataAnnotations;
public class Purchase
{
    [Key]
    [Required]
    public int Id { get; set; }

    [Required]
    public int Quantity {get;set;}
    [Required]
    public string Buyer { get; set; }

    [Required]
    public int ProductId { get; set; }
    public Product product {get; set;}
}