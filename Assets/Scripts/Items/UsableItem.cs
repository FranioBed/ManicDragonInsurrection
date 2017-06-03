using System;

public class UsableItem : Item
{
    public bool IsUsed = false;
    public UsableItem(string name, string description, string spritePath) : base(name, description, spritePath)
    {
        
    }

    public void OnUse(Player player)
    {
        IsUsed = true;
        throw new NotImplementedException();
        foreach (var feature in features)
        {
            feature.Activate();
        }
    }
}
