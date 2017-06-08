using System;

public class EquippableItem : Item
{
    public enum EquippableType { Armor, Weapon }
    public EquippableType Type { get; set; }

    public EquippableItem(string name, int uniqueId, string description, EquippableType type, string spritePath)
        : base(name, uniqueId, description, spritePath)
    {
        Type = type;
    }
    public void Equip(Player player)
    {
        foreach (var feature in features)
        {
            feature.Activate(player);
        }
    }

    public void UnEquip(Player player)
    {
        foreach (var feature in features)
        {
            feature.Deactivate(player);
        }
    }
}
