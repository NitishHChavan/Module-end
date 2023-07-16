using System.ComponentModel.DataAnnotations;

public class Car
{
    [Key]
    public int CarId { get; set; }

    [Required]
    public string Name { get; set; }

    public int bhp { get; set; }

    public int cc { get; set; }

    public int FuelTank { get; set; }

    public int FuelEconomy { get; set; }
}
