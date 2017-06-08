using UnityEngine;

public abstract class Feature
{
    public enum FeatureTypes { Instant, Continues, Temp }
    public float Amount { get; set; }
    public StatTypes Stat { get; set; }
    public FeatureTypes Type { get; set; }

    protected Feature(float amount, StatTypes stat, FeatureTypes type)
    {
        this.Amount = amount;
        this.Stat = stat;
        this.Type = type;
    }

    public virtual void Activate(Player player) { }
    public virtual void Deactivate(Player player) { }
}
