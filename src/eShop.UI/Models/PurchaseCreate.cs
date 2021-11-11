using System.ComponentModel.DataAnnotations;

public class PurchaseCreate
{
    [Required]
    public int Quantity {get;set;}
    [Required]
    public string Buyer { get; set; }
}