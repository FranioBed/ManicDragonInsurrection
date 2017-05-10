using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Equipment : MonoBehaviour
{
    private const int FastAccessSize = 3;
    private const int InventorySize = 10;

    private List<Usable> fastAccess = new List<Usable>( new Usable[FastAccessSize] );
    private Equippable armor;
    private Equippable weapon;

    
    private List<Item> inventory = new List<Item>(new Item[InventorySize]);
}
