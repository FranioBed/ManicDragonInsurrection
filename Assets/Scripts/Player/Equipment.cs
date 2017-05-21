using System.Collections.Generic;
using UnityEngine;

public class Equipment : MonoBehaviour
{
    private const int FastAccessSize = 3;
    private const int InventorySize = 10;

    private List<UsableItem> fastAccess = new List<UsableItem>( new UsableItem[FastAccessSize] );
    private EquippableItem armor;
    private EquippableItem weapon;

    
    private List<Item> inventory = new List<Item>(new Item[InventorySize]);
}
