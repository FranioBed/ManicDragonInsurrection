using System.Collections.Generic;
using UnityEngine;

public abstract class Item : MonoBehaviour
{
    public string Name { get; set; }
    public string Description { get; set; }
    public Sprite Miniature { get; set; }

    public List<Feature> features;

    protected Item(string name, string description)
    {
        Name = name;
        Description = description;
    }
    /*
    public bool IsArcherItem { get; private set; }
    public bool IsMageItem { get; private set; }
    public bool IsWarriorItem { get; private set; }
    */
}
