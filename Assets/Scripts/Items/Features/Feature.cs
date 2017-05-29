using UnityEngine;

public abstract class Feature : MonoBehaviour
{
    public enum FeatureTypes { Instant, Continues, Temp }
    public int Amount { get; private set; }
    public StatTypes Stat { get; private set; }
    public FeatureTypes Type { get; set; }

    protected Feature(int amount, StatTypes stat, FeatureTypes type)
    {
        Amount = amount;
        Stat = stat;
        Type = type;
    }

    public virtual void Activate() { }
    public virtual void Deactivate() { }
}
