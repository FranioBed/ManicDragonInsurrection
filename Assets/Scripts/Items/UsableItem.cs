public class UsableItem : Item {
    public void OnUse()
    {
        foreach (var feature in features)
        {
            feature.Activate();
        }
    }
}
