using System;

public class InstantStatModFeature : Feature
{
    public InstantStatModFeature(float amount, StatTypes stat, FeatureTypes type)
        : base(amount, stat, type)
    {

    }

    public override void Activate(Player player)
    {
        if (Stat == StatTypes.Health)
            player.Health += Amount;
        else if (Stat == StatTypes.Mana)
            player.Mana += Amount;
        else if (Stat == StatTypes.Strength)
            throw new NotImplementedException();
        else if (Stat == StatTypes.Agility)
            player.speed += Amount;
        else if (Stat == StatTypes.WeaponDamage)
            throw new NotImplementedException();
    }

    public override void Deactivate(Player player)
    {
        if (Stat == StatTypes.Health)
            player.Health -= Amount;
        else if (Stat == StatTypes.Mana)
            player.Mana -= Amount;
        else if (Stat == StatTypes.Strength)
            throw new NotImplementedException();
        else if (Stat == StatTypes.Agility)
            player.speed -= Amount;
        else if (Stat == StatTypes.WeaponDamage)
            throw new NotImplementedException();
    }
}
