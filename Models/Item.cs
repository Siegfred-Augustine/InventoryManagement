namespace InventoryManagement;

public class Item
{
  public Guid id{get;set;}
  public string? itemName{get;set;}
  public string? itemDesc{get;set;}
  public int itemCount{get;set;}
  public float itemVal{get;set;}
}
