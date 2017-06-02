public class EquippableItem : Item {
    public enum EquippableType { Armor, Weapon }
    public EquippableType Type { get; set; }

    public EquippableItem(string name, string description, EquippableType type) : base(name, description)
    {
        Type = type;
    }
    public void Equip()
    {
        foreach (var feature in features)
        {
            feature.Activate();
        }
    }

    public void UnEquip()
    {
        foreach (var feature in features)
        {
            feature.Deactivate();
        }
    }
}
