using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class ItemPrefabScript : MonoBehaviour {

    public UsableItem Item { get; set; }

	void Start ()
	{
	    Item = ItemsManager.GetRandomUsableItem();
	    gameObject.GetComponentInChildren<SpriteRenderer>().sprite = Resources.Load<Sprite>(Item.Miniature);
        gameObject.GetComponentInChildren<TextMesh>().text = Item.Name;
    }
	
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.gameObject.GetComponent<Player>().AddItemToInventory(Item);
            Destroy(gameObject);
        }
    }
}
