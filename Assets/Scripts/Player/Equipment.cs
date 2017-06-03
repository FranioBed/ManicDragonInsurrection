using System.Collections.Generic;

public class Equipment
{
    private const int FastAccessSize = 3;
    private const int InventorySize = 10;

    public List<UsableItem> fastAccess = new List<UsableItem>( new UsableItem[FastAccessSize] );
    public EquippableItem armor;
    public EquippableItem weapon;

    public List<Item> inventory = new List<Item>(new Item[InventorySize]);
}
