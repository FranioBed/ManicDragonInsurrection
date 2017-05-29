public class EquippableItem : Item {
    public EquippableItem(string name, string description) : base(name, description)
    {

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
