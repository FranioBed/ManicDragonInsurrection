using System;
using System.Collections;
using UnityEngine;

public class TempStatModFeature : Feature {
    public float Duration { get; set; }

    public TempStatModFeature(float duration, float amount, StatTypes stat, FeatureTypes type)
        : base(amount, stat, type)
    {
        Duration = duration;
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
            throw new NotImplementedException();
        else if (Stat == StatTypes.WeaponDamage)
            throw new NotImplementedException();
        StartCoroutine(Finish(player));
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
            throw new NotImplementedException();
        else if (Stat == StatTypes.WeaponDamage)
            throw new NotImplementedException();
    }

    private IEnumerator Finish(Player player)
    {
        Deactivate(player);
        yield return new WaitForSeconds(Duration);
    }
}
