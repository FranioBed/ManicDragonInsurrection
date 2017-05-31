public class TempStatModFeature : Feature {
    public float Duration { get; set; }

    public TempStatModFeature(float duration, float amount, StatTypes stat, FeatureTypes type)
        : base(amount, stat, type)
    {
        Duration = duration;
    }
}
