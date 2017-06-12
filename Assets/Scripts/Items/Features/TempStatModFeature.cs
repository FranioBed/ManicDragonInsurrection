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
            throw new NotImplementedException();
        else if (Stat == StatTypes.Mana)
            throw new NotImplementedException();
        else if (Stat == StatTypes.Strength)
            throw new NotImplementedException();
        else if (Stat == StatTypes.Agility)
            player.TempSpeedIncrease(Amount, Duration);
        else if (Stat == StatTypes.WeaponDamage)
            throw new NotImplementedException();
        //StartCoroutine(Finish(player));
    }
}
