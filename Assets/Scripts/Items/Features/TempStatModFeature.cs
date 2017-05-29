public class TempStatModFeature : Feature {
    public float Duration { get; private set; }

    public TempStatModFeature(float duration, int amount, StatTypes stat, FeatureTypes type)
        : base(amount, stat, type)
    {
        Duration = duration;
    }
}
