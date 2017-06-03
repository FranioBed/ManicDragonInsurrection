using System;

public class EquippableItem : Item {
    public enum EquippableType { Armor, Weapon }
    public EquippableType Type { get; set; }

    public EquippableItem(string name, string description, EquippableType type, string spritePath) : base(name, description, spritePath)
    {
        Type = type;
    }
    public void Equip(Player player)
    {
        throw new NotImplementedException();
        foreach (var feature in features)
        {
            feature.Activate();
        }
    }

    public void UnEquip(Player player)
    {
        throw new NotImplementedException();
        foreach (var feature in features)
        {
            feature.Deactivate();
        }
    }
}
