using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootChest : MonoBehaviour {

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            GameObject instance = Instantiate(Resources.Load("Prefabs/Items/UsableItemPrefab")) as GameObject;
            instance.transform.position = gameObject.transform.position;
            Destroy(gameObject);
        }
            
    }
}
