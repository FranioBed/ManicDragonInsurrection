using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public float speed = 1f;
    public float attackSpeed = 0.5f;
    public float Health { get; set; }
    public float Mana { get; set; }
    public GameObject basicAttackPrefab;
    public Text healthTextUI;
    public Text manaTextUI;
    public Slider healthSlider;
    public Slider manaSlider;

    private Rigidbody2D rb2D;
    private Equipment equipment = new Equipment();
    private float basicAttackCooldown = 0.0f;
    private AnimationController animController;
    private State state;

    void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();
        Health = 100;
        Mana = 100;
        animController = GetComponent<AnimationController>();
        state = State.Idle;

    }

    private void Update()
    {
        healthTextUI.text = Health.ToString();
        manaTextUI.text = Mana.ToString();
        healthSlider.value = Health;
        manaSlider.value = Mana;

        basicAttackCooldown = (basicAttackCooldown <= 0 ? basicAttackCooldown = 0 : basicAttackCooldown - Time.deltaTime);
        if (Input.GetMouseButton(0) && basicAttackCooldown == 0)
        {
            basicAttackCooldown = attackSpeed;
            Vector2 target = Camera.main.ScreenToWorldPoint(new Vector2(Input.mousePosition.x, Input.mousePosition.y));
            Vector2 myPos = new Vector2(transform.position.x, transform.position.y);
            Vector2 direction = target - myPos;
            direction.Normalize();
            GameObject projectile = (GameObject)Instantiate(basicAttackPrefab, myPos, Quaternion.identity);
            projectile.GetComponent<Rigidbody2D>().velocity = direction * projectile.GetComponent<Projectile>().speed;

            projectile.transform.LookAt(transform.position + new Vector3(0, 0, 1), direction);
            state = State.Fight;

        }

        FastAccessInput();

        UpdateAnimationState();
    }

    void FixedUpdate()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        //Debug.Log("H: " + horizontal + ", V: " + vertical);
        Vector2 direction = new Vector2(horizontal, vertical);
        rb2D.velocity = (direction.magnitude > 1) ? direction.normalized * speed : direction * speed;
        //rb2D.AddForce(direction * speed);

        if (rb2D.velocity.magnitude > 1)
            state = State.Walk;
        else if (state != State.Fight && state != State.Die)
            state = State.Idle;
    }

    void UpdateAnimationState()
    {
        if (state == State.Idle)
        {
            animController.SetAnimationState(AnimationState.IDLE);
        }
        else if (state == State.Walk)
        {
            animController.SetAnimationState(AnimationState.WALK);
        }
        else if (state == State.Fight)
        {
            animController.SetAnimationState(AnimationState.FIGHT);
        }
        else if (state == State.Die)
        {
            animController.SetAnimationState(AnimationState.DIE);
        }
    }

    private void FastAccessInput()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            if (equipment.fastAccess[0] != null)
            {
                equipment.fastAccess[0].OnUse(this);
                //TODO: decrement
                equipment.fastAccess[0] = null;
            }
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            if (equipment.fastAccess[1] != null)
            {
                equipment.fastAccess[1].OnUse(this);
                //TODO: decrement
                equipment.fastAccess[0] = null;
            }
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            if (equipment.fastAccess[2] != null)
            {
                equipment.fastAccess[2].OnUse(this);
                //TODO: decrement
                equipment.fastAccess[0] = null;
            }
        }
    }

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

    public void AddItemToInventory(EquippableItem newItem)
    {
        equipment.inventory.Add(newItem);
    }

    public void AddItemToInventory(UsableItem newItem)
    {
        equipment.inventory.Add(newItem);
        //TODO: remove: (only for tests)
        EquipItemToFastAccess(0, newItem);
    }

    public void RemoveItemFromInventory(Item item)
    {
        for (int i = 0; i < equipment.inventory.Count; i++)
        {
            if (equipment.inventory[i] != null && equipment.inventory[i].UniqueId == item.UniqueId)
            {
                //TODO: replace by .Find
                equipment.inventory.RemoveAt(i);
            }
        }
    }

    public void GetDamage(float damage)
    {
        //TODO: obniżyć obrażenia o armor
        Health -= damage;
    }

    public enum State
    {
        Idle,
        Walk,
        Fight,
        Die,
    }

    public void ConstantlyHealthIncrease(float amount, float interval, float duration)
    {
        StartCoroutine(AddHealthConstantly(amount, interval, duration));
    }

    public void ConstantlyManaIncrease(float amount, float interval, float duration)
    {
        StartCoroutine(AddManaConstantly(amount, interval, duration));
    }

    private IEnumerator AddHealthConstantly(float amount, float interval, float duration)
    {
        while (duration > 0)
        {
            duration -= interval;
            Health += amount;

            yield return new WaitForSeconds(interval);
        }
    }

    private IEnumerator AddManaConstantly(float amount, float interval, float duration)
    {
        while (duration > 0)
        {
            duration -= interval;
            Mana += amount;

            yield return new WaitForSeconds(interval);
        }
    }
}
