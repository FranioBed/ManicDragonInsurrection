public class ContinuesStatModFeature : Feature
{
    public float Duration { get; private set; }
    public float Interval { get; private set; }

    public ContinuesStatModFeature(float duration, float interval, int amount, StatTypes stat, FeatureTypes type)
        : base(amount, stat, type)
    {
        Duration = duration;
        Interval = interval;
    }
}
