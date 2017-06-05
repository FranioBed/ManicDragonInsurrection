using System;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float speed = 1f;
    public float attackSpeed = 0.5f;
    public float Health { get; set; }
    public float Mana { get; set; }
    public GameObject basicAttackPrefab;

    private Rigidbody2D rb2D;
    private Equipment equipment = new Equipment();

    void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();
        Health = 100;
        Mana = 100;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log("Mouse button pressed");
            Vector2 target = Camera.main.ScreenToWorldPoint(new Vector2(Input.mousePosition.x, Input.mousePosition.y));
            Vector2 myPos = new Vector2(transform.position.x, transform.position.y);
            Vector2 direction = target - myPos;
            direction.Normalize();
            GameObject projectile = (GameObject)Instantiate(basicAttackPrefab, myPos, Quaternion.identity);
            projectile.GetComponent<Rigidbody2D>().velocity = direction * projectile.GetComponent<Projectile>().speed;
            
        }
    }

    void FixedUpdate()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        //Debug.Log("H: " + horizontal + ", V: " + vertical);
        Vector2 direction = new Vector2(horizontal, vertical);
        rb2D.velocity = (direction.magnitude > 1) ? direction.normalized * speed : direction * speed;
        //rb2D.AddForce(direction * speed);
    }

    //--------------------------------------------------------------------------------------------------------
    public void UseFastAccessItem(int id)
    {
        equipment.fastAccess[id].OnUse(this);
        //equipment.fastAccess[id] = null;
    }

    public void EquipItemToFastAccess(int id, UsableItem newItem)
    {
        if (equipment.fastAccess[id] != null)
            equipment.inventory.Add(equipment.fastAccess[id]);
        equipment.fastAccess[id] = newItem;
        RemoveItemFromInventory(newItem);
    }

    private void EquipWeapont(EquippableItem newWeapon)
    {
        equipment.inventory.Add(equipment.weapon);
        equipment.weapon.UnEquip(this);
        equipment.weapon = newWeapon;
        equipment.weapon.Equip(this);
        RemoveItemFromInventory(newWeapon);
    }

    private void EquipArmor(EquippableItem newArmor)
    {
        equipment.inventory.Add(equipment.armor);
        equipment.armor.UnEquip(this);
        equipment.armor = newArmor;
        equipment.armor.Equip(this);
        RemoveItemFromInventory(newArmor);
    }

    public void UnEquipWeapon()
    {
        equipment.inventory.Add(equipment.weapon);
        equipment.weapon.UnEquip(this);
        equipment.weapon = null;
    }

    public void UnEquipArmor()
    {
        equipment.inventory.Add(equipment.armor);
        equipment.armor.UnEquip(this);
        equipment.armor = null;
    }

    private void AddItemToInventory(Item newItem)
    {
        equipment.inventory.Add(newItem);
    }

    public void RemoveItemFromInventory(Item item)
    {
        //TODO:
        throw new NotImplementedException();
    }

	public void GetDamage(float damage) {
		//TODO: obniżyć obrażenia o armor
		Health -= damage;
	}

    //--------------------------------------------------------------------------------------------------------
}
