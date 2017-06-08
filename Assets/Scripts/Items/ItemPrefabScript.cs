using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPrefabScript : MonoBehaviour {

    public UsableItem Item { get; set; }

	void Start ()
	{
	    Item = ItemsManager.GetRandomUsableItem();
	    gameObject.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>(Item.Miniature);
	}
	
	// Update is called once per frame
	void Update () {
		
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
