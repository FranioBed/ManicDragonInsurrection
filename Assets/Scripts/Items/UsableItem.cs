using System;

public class UsableItem : Item
{
    public UsableItem(string name, int uniqueId, string description, string spritePath)
        : base(name, uniqueId, description, spritePath)
    {

    }

    public void OnUse(Player player)
    {
        foreach (var feature in features)
            feature.Activate(player);
    }
}
