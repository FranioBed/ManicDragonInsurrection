using System;
using System.Collections;
using UnityEngine;

public class ContinuesStatModFeature : Feature
{
    public float Duration { get; private set; }
    public float Interval { get; private set; }

    public ContinuesStatModFeature(float duration, float interval, float amount, StatTypes stat, FeatureTypes type)
        : base(amount, stat, type)
    {
        Duration = duration;
        Interval = interval;
    }

    public override void Activate(Player player)
    {
        //InvokeRepeating("Test", 2.0f, 0.3f);
        if (Stat == StatTypes.Health)
            player.ConstantlyHealthIncrease(Amount, Interval, Duration);
        else if (Stat == StatTypes.Mana)
            player.ConstantlyManaIncrease(Amount, Interval, Duration);
        else if (Stat == StatTypes.Strength)
            throw new NotImplementedException();
        else if (Stat == StatTypes.Agility)
            throw new NotImplementedException();
        else if (Stat == StatTypes.WeaponDamage)
            throw new NotImplementedException();

        //if (Duration > 0)
        //StartCoroutine(Add(player));
    }

//    private IEnumerator Add(Player player)
//    {
//        while (Duration > 0)
//        {
//            Duration -= Interval;
//            if (Stat == StatTypes.Health)
//                player.Health += Amount;
//            else if (Stat == StatTypes.Mana)
//                player.Mana += Amount;
//            else if (Stat == StatTypes.Strength)
//                throw new NotImplementedException();
//            else if (Stat == StatTypes.Agility)
//                throw new NotImplementedException();
//            else if (Stat == StatTypes.WeaponDamage)
//                throw new NotImplementedException();
//
//            yield return new WaitForSeconds(Interval);
//        }
//    }
}
