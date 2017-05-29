public class UsableItem : Item {
    public UsableItem(string name, string description) : base(name, description)
    {
        
    }

    public void OnUse()
    {
        foreach (var feature in features)
        {
            feature.Activate();
        }
    }
}
