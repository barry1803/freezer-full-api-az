namespace WebApi.Entities;
public class FoodItem
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public string? Description { get; set; }
    public string? DateFrozen { get; set; }
    public int Quantity { get; set; }
    public string? FreezerLocation { get; set; }
    public string? ItemLocation { get; set; }
}