using System.Collections.Generic;
using UnityEngine;

public abstract class Item
{
    public string Name { get; set; }
    public string Description { get; set; }
    public Sprite Miniature { get; set; }

    public List<Feature> features = new List<Feature>();

    protected Item(string name, string description, string spritePath)
    {
        Name = name;
        Description = description;
        //TODO:
        //Miniature = Resources.Load<Sprite>(spriteName);
    }
    /*
    public bool IsArcherItem { get; private set; }
    public bool IsMageItem { get; private set; }
    public bool IsWarriorItem { get; private set; }
    */
}
