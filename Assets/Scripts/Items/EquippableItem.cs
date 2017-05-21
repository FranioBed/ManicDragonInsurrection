public class EquippableItem : Item {

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
