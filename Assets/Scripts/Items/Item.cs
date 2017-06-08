using System.Collections.Generic;
using UnityEngine;

public abstract class Item
{
    public string Name { get; set; }
    public int UniqueId { get; set; }
    public string Description { get; set; }
    public string Miniature { get; set; }

    public List<Feature> features = new List<Feature>();

    protected Item(string name, int uniqueId, string description, string spritePath)
    {
        Name = name;
        UniqueId = uniqueId;
        Description = description;
        Miniature = @"Images/Items/" + spritePath;
    }
    /*
    public bool IsArcherItem { get; private set; }
    public bool IsMageItem { get; private set; }
    public bool IsWarriorItem { get; private set; }
    */
}
