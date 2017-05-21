using System.Collections.Generic;
using UnityEngine;

public class ItemsManager : MonoBehaviour
{
    private const string usableItemsJsonFile = "/Resources/JSON Files/Items/usable-items.json";
    private const string equippableItemsJsonFile = "/Resources/JSON Files/Items/equippable-items.json";
    private List<UsableItem> usableItems;
    private List<EquippableItem> exuippableItems;
    void Awake ()
	{
	    LoadUsableItemsListFromFile(usableItemsJsonFile);
        LoadEquippableItemsListFromFile(equippableItemsJsonFile);
    }

    private void LoadEquippableItemsListFromFile(string path)
    {
        throw new System.NotImplementedException();
    }

    private void LoadUsableItemsListFromFile(string path)
    {
        throw new System.NotImplementedException();
    }
}
