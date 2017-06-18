using System;
using System.Collections;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float speed = 1f;
    public float attackSpeed = 0.5f;
    public float Health { get; set; }
    public float Mana { get; set; }
    public GameObject basicAttackPrefab;
    public UIManager uiManager;

    private Rigidbody2D rb2D;
    private Equipment equipment = new Equipment();
    private float basicAttackCooldown = 0.0f;
    private float secondaryAttackCooldown = 5.0f;
    private float secondaryAttackCooldownTimer = 0.0f;
    private AnimationController animController;
    private State state;
    private bool shield;
    private int maxMana = 100;
    private int maxHealth = 100;
    private float manaRegenPerSecond = 0.5f;
    private float healthRegenPerSecond = 0.2f;
    private bool primarySkillAvailability = true;
    private bool secondarySkillAvailability = true;
    private bool tertriarySkillAvailability = true;

    public enum Ability
    {
        PRIMARY = 0,
        SECONDARY = 1,
        TERTIARY = 2
    }
    public delegate void ValueChangedEventHandler(object sender, int value);
    public event ValueChangedEventHandler HealthChanged;
    public event ValueChangedEventHandler ManaChanged;
    public delegate void AvailabilityChangedEventHandler(object sender, Ability ability, bool available, float cooldown);
    public event AvailabilityChangedEventHandler AbilityAvailabilityChanged;
    public delegate void FastActionSlotChangedHandler(object sender, int fastActionSlotIndex, string itemMinature);
    public event FastActionSlotChangedHandler FastActionSlotChanged;
    public delegate void VoidEventHandler(object sender);
    public event VoidEventHandler OnDeath;

    void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();
        Health = maxHealth;
        Mana = maxMana;
        animController = GetComponent<AnimationController>();
        state = State.Idle;
        HealthChanged(this, (int) Math.Floor(Health));
        ManaChanged(this, (int) Math.Floor(Mana));
        AbilityAvailabilityChanged(this, Ability.PRIMARY, true, 1f);
        AbilityAvailabilityChanged(this, Ability.SECONDARY, true, 1f);
        AbilityAvailabilityChanged(this, Ability.TERTIARY, true, 1f);
        FastActionSlotChanged(this, 0, null);
        FastActionSlotChanged(this, 1, null);
        FastActionSlotChanged(this, 2, null);
    }

    private void Update()
    {
        PassiveRegeneration(Time.deltaTime);
        HealthChanged(this, (int)Math.Floor(Health));
        ManaChanged(this, (int)Math.Floor(Mana));
        basicAttackCooldown = (basicAttackCooldown <= 0 ? basicAttackCooldown = 0 : basicAttackCooldown - Time.deltaTime);
        secondaryAttackCooldownTimer = (secondaryAttackCooldownTimer <= 0 ? secondaryAttackCooldownTimer = 0 : secondaryAttackCooldownTimer - Time.deltaTime);
        updateSkillStatuses();
        useSkillsAndUpdateSkillStatuses();
        FastAccessInput();
        UpdateAnimationState();
    }

    private void useSkillsAndUpdateSkillStatuses()
    {
        if (Input.GetAxis("Fire1") > 0 && primarySkillAvailability)
        {
            BasicAttack();
            if (!isPrimaryAttackAvaiable())
            {
                primarySkillAvailability = false;
                AbilityAvailabilityChanged(this, Ability.PRIMARY, false, 0f);
            }
        }
        if (Input.GetAxis("Fire2") > 0 && secondarySkillAvailability)
        {
            SecondaryAttack();
            if (!isSecondaryAttackAvaiable())
            {
                secondarySkillAvailability = false;
                AbilityAvailabilityChanged(this, Ability.SECONDARY, false, 0f);
            }
        }
        if (Input.GetAxis("Fire3") > 0 && tertriarySkillAvailability)
        {
            DefensiveSkill();
            if (!isThirdAttackAvaiable())
            {
                tertriarySkillAvailability = false;
                AbilityAvailabilityChanged(this, Ability.TERTIARY, false, 1f);
            }
        }
        else
        {
            shield = false;
            GetComponent<SpriteRenderer>().color = Color.white;
        }
    }

    private void updateSkillStatuses()
    {
        if (!primarySkillAvailability)
        {
            if (isPrimaryAttackAvaiable())
            {
                primarySkillAvailability = true;
                AbilityAvailabilityChanged(this, Ability.PRIMARY, true, 1f);
            }
            else
            {
                AbilityAvailabilityChanged(this, Ability.PRIMARY, false, 1f - basicAttackCooldown / attackSpeed);
            }
        }
        if (!secondarySkillAvailability)
        {
            if (isSecondaryAttackAvaiable())
            {
                secondarySkillAvailability = true;
                AbilityAvailabilityChanged(this, Ability.SECONDARY, true, 1f);
            }
            else
            {
                AbilityAvailabilityChanged(this, Ability.SECONDARY, false, 1f - secondaryAttackCooldownTimer / secondaryAttackCooldown);
            }
        }
        if (!tertriarySkillAvailability)
        {
            if (isThirdAttackAvaiable())
            {
                tertriarySkillAvailability = true;
                AbilityAvailabilityChanged(this, Ability.TERTIARY, true, 1f);
            }
        }
    }

    private bool isPrimaryAttackAvaiable()
    {
        return basicAttackCooldown == 0;
    }

    private bool isSecondaryAttackAvaiable()
    {
        return Mana >= 20 && secondaryAttackCooldownTimer == 0;
    }

    private bool isThirdAttackAvaiable()
    {
        return Mana > 0.1;
    }

    private void SecondaryAttack()
    {
        //TODO: put in correct place
        float manaCost = 20;
        float radius = 5;
        float strength = 3;
        float damage = 3;
        secondaryAttackCooldownTimer = secondaryAttackCooldown;
        Mana -= manaCost;
        Collider2D[] hit = Physics2D.OverlapCircleAll(transform.position, radius);
        foreach (Collider2D coll in hit)
        {
            if(coll.tag == "Enemy")
            {
                coll.GetComponent<Enemy>().RecieveDamage(damage);
                Vector2 direction = coll.transform.position - transform.position;
                direction.Normalize();
                direction *= strength * 10;
                coll.GetComponent<Enemy>().RecieveKnockback(1, direction);
            }
        }
    }

    private void DefensiveSkill()
    {
        //TODO: put in correct place
        float manaPerSecond = 10f;
        Mana -= manaPerSecond * Time.deltaTime;
        shield = true;
        //visual cue for shield, TODO: replace with additional sprite?
        GetComponent<SpriteRenderer>().color = Color.blue;
    }

    void BasicAttack()
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

    void FixedUpdate()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        //Debug.Log("H: " + horizontal + ", V: " + vertical);
        Vector2 direction = new Vector2(horizontal, vertical);
        rb2D.velocity = (direction.magnitude > 1) ? direction.normalized * speed : direction * speed;
        //rb2D.AddForce(direction * speed);

        if(horizontal != 0)
        {
            if (horizontal > 0) FlipCharacter(false);
            else FlipCharacter(true);
        }

        if (rb2D.velocity.magnitude > 0)
            state = State.Walk;
        else if (state != State.Fight && state != State.Die)
            state = State.Idle;
    }

    void FlipCharacter(bool flip)
    {
        SpriteRenderer rend = GetComponent<SpriteRenderer>();
        rend.flipX = flip;
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
                FastActionSlotChanged(this, 0, "");
                equipment.fastAccess[0].OnUse(this);
                //TODO: decrement
                equipment.fastAccess[0] = null;
            }
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            if (equipment.fastAccess[1] != null)
            {
                FastActionSlotChanged(this, 1, "");
                equipment.fastAccess[1].OnUse(this);
                //TODO: decrement
                equipment.fastAccess[1] = null;
            }
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            if (equipment.fastAccess[2] != null)
            {
                FastActionSlotChanged(this, 2, "");
                equipment.fastAccess[2].OnUse(this);
                //TODO: decrement
                equipment.fastAccess[2] = null;
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
        FastActionSlotChanged(this, id, newItem.Miniature);
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

    public bool AddItemToInventory(UsableItem newItem)
    {
        equipment.inventory.Add(newItem);
        //TODO: remove: (only for tests)
        if (equipment.fastAccess[0] == null)
        {
            EquipItemToFastAccess(0, newItem);
            return true;
        }
        if (equipment.fastAccess[1] == null)
        {
            EquipItemToFastAccess(1, newItem);
            return true;
        }
        if (equipment.fastAccess[2] == null)
        {
            EquipItemToFastAccess(2, newItem);
            return true;
        }
        return false;
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

    public void RecieveDamage(float damage)
    {
        //TODO: obniżyć obrażenia o armor
        if (!shield)
        {
            Health -= damage;
        }
        if (Health < 0)
        {
            state = State.Die;
            OnDeath(this);
        }
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

    public void TempSpeedIncrease(float amount, float duration)
    {
        speed += amount;
        StartCoroutine(TempSpeedTimer(amount, duration));
    }

    private IEnumerator AddHealthConstantly(float amount, float interval, float duration)
    {
        while (duration > 0)
        {
            duration -= interval;
            Health = (Health > maxHealth ? maxHealth : Health + amount);

            yield return new WaitForSeconds(interval);
        }
    }

    private IEnumerator AddManaConstantly(float amount, float interval, float duration)
    {
        while (duration > 0)
        {
            duration -= interval;
            Mana = (Mana > maxMana ? maxMana : Mana + amount);

            yield return new WaitForSeconds(interval);
        }
    }

    private IEnumerator TempSpeedTimer(float amount, float duration)
    {
        yield return new WaitForSeconds(duration);
        speed -= amount;
    }

    private void PassiveRegeneration(float delta)
    {
        Mana = (Mana >= maxMana ? maxMana : Mana + manaRegenPerSecond * delta);
        Health = (Health >= maxHealth ? maxHealth : Health + healthRegenPerSecond * delta);
    }
}
